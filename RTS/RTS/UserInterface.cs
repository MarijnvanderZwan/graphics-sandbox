using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RTS
{
    public class UserInterface : DrawableGameComponent
    {
        SelectionRectangle selectionRect;
        public Camera Camera;
        public ControlState ControlState;
        Plane plane = new Plane(new Vector3(0, 1, 0), 0);

        public UserInterface(Game1 game) : base(game)
        {
            Instance = this;
            ControlState = new ControlState();
            Camera = new Camera(new Vector3(0, 100, 100), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        }

        public override void Initialize()
        {
            selectionRect = new SelectionRectangle();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // Camera panning
            if (InputState.IsKeyDown(Keys.A))
                Camera.Move(-1f, 0);
            if (InputState.IsKeyDown(Keys.D))
                Camera.Move(1f, 0);
            if (InputState.IsKeyDown(Keys.W))
                Camera.Move(0, -1f);
            if (InputState.IsKeyDown(Keys.S))
                Camera.Move(0, 1f);

            // Camera zooming
            if (InputState.DeltaMouseScrollWheelValue > 0)
                Camera.Zoom(1 / 1.1f);
            else if (InputState.DeltaMouseScrollWheelValue < 0)
                Camera.Zoom(1.1f);

            if (InputState.MouseLeftButtonDown && InputState.oldMouseState.LeftButton == ButtonState.Released)
                selectionRect.Position = new Vector3(InputState.MousePosition.X, InputState.MousePosition.Y, 0);
            else if (InputState.MouseLeftButtonDown)
            {
                selectionRect.Scale.X = InputState.MousePosition.X - selectionRect.Position.X;
                selectionRect.Scale.Y = InputState.MousePosition.Y - selectionRect.Position.Y;
            }
            else if (InputState.MouseLeftButtonPressed)
            {
                selectionRect.Scale.X = InputState.MousePosition.X - selectionRect.Position.X;
                selectionRect.Scale.Y = InputState.MousePosition.Y - selectionRect.Position.Y;
                // Select 
                Vector3 p1 = selectionRect.Position;
                Vector3 p2 = selectionRect.Position + selectionRect.Scale;

                Vector3 projectedP1 = Camera.ProjectionToPlane(p1);
                Vector3 projectedP2 = Camera.ProjectionToPlane(p2);

                Rect rect = new Rect(projectedP1.X, projectedP1.Z, projectedP2.X, projectedP2.Z);
                World.Instance.Army.SelectUnits(rect);

                selectionRect.Scale.X = 0;
                selectionRect.Scale.Y = 0;
            }

            if (ControlState.HotKey == HotKeys.Shift && InputState.MouseRightButtonPressed)
                World.Instance.Army.QueueMoveSelectedUnits(Camera.ProjectionToPlane(InputState.MousePosition));
            else if (InputState.MouseRightButtonPressed)
                World.Instance.Army.MoveSelectedUnits(Camera.ProjectionToPlane(InputState.MousePosition));

            Camera.UpdateViewMatrix();
            ControlState.Update();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            selectionRect.Draw();

            base.Draw(gameTime);
        }


        public static UserInterface Instance
        {
            get;
            private set;
        }
      
    }
}
