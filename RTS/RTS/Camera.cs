using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RTS
{
    public class Camera
    {
        Matrix viewMatrix;
        Matrix projectionMatrix;

        Vector3 eye;
        Vector3 focus;

        Plane upPlane;

        Viewport viewport;

        public Camera(Vector3 camEye, Vector3 camFocus, Vector3 camUp, float aspectRatio = 4.0f / 3.0f)
        {
            eye = camEye;
            focus = camFocus;
            upPlane = new Plane(camUp, 0);
            viewport = new Viewport(0, 0, 1024, 768);

            viewMatrix = Matrix.CreateLookAt(Eye, Focus, Up);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 1000.0f);
            UpdateViewMatrix();
        }

        public void UpdateViewMatrix()
        {
            viewMatrix = Matrix.CreateLookAt(eye, focus, Up);
        }

        public void Zoom(float amount)
        {
            Vector3 focusToEye = Eye - Focus;
            float distance = focusToEye.Length();

            if (distance * amount > 1.0f)
                Eye = Focus + focusToEye * amount;
        }

        public Vector3 Eye
        {
            get { return eye; }
            set { eye = value; UpdateViewMatrix(); }
        }

        public Vector3 Focus
        {
            get { return focus; }
            set { focus = value; UpdateViewMatrix(); }
        }

        public Vector3 LookAt
        {
            get { return Vector3.Normalize(Focus - Eye); }
        }

        public Vector3 Side
        {
            get { return Vector3.Normalize(Vector3.Cross(LookAt, Up)); }
        }

        public Vector3 Up
        {
            get
            {
                Vector3 side = Vector3.Cross(Vector3.UnitY, LookAt);
                Vector3 up = Vector3.Cross(LookAt, side);
                up.Normalize();
                return up;
            }
        }

        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }

        public Matrix ProjectionMatrix
        {
            get { return projectionMatrix; }
        }

        public Vector3 GetScreenCoordinates(Vector3 point)
        {
            return viewport.Project(point, ProjectionMatrix, viewMatrix, Matrix.Identity);
        }

        public void Move(float x, float y)
        {
            Eye += Side * x;
            Eye += Vector3.Cross(Side, Vector3.UnitY) * y;

            Focus += Side * x;
            Focus += Vector3.Cross(Side, Vector3.UnitY) * y;

        }

        public Vector3 Unproject(int x, int y, int z)
        {            
            return viewport.Unproject(new Vector3(x, y, z), projectionMatrix, viewMatrix, Matrix.Identity);
        }

        public Vector3 ProjectionToPlane(int x, int y)
        {
            Vector3 near = Unproject(x, y, 0);
            Vector3 far = Unproject(x, y, 1);


            Ray r = new Ray(near, Vector3.Normalize(far - near));
            float? intersection = r.Intersects(upPlane);
            if (intersection.HasValue)
                return intersection.Value * Vector3.Normalize(far - near) + near;

            return new Vector3();
        }

        public Vector3 ProjectionToPlane(Vector3 v)
        {
            return ProjectionToPlane((int)v.X, (int)v.Y);
        }

        public Vector3 ProjectionToPlane(Vector2 v)
        {
            return ProjectionToPlane((int)v.X, (int)v.Y);
        }
    }
}
    