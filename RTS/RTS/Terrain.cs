using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace RTS
{
    public class Terrain : GameObjectTextured
    {
        public float[] Height = new float[0];
        public VertexPositionNormalTexture[] terrain;

        public Terrain() : base()
        {
        }

        public void VerticesFromHeightMap(Texture2D heightMap)
        {
            int width = heightMap.Width;
            int height = heightMap.Height;

            Color[] heightMapColors = new Color[width * height];
            heightMap.GetData(heightMapColors);

            Height = new float[width * height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    Height[x + y * width] = heightMapColors[x + y * width].R / 5.0f;
        }

        public void TerrainToVertices()
        {

            int width = (int)Math.Sqrt(Height.Length);
            terrain = new VertexPositionNormalTexture[width * width];
            List<VertexPositionNormalTexture> vertices = new List<VertexPositionNormalTexture>();
            for (int x = 0; x < width; x++)
                for (int y = 0; y < width; y++)
                {
                    Vector3 position = new Vector3(x - width / 2, Height[x + y * width], y - width / 2);
                    Vector2 textureCoords = new Vector2();
                    textureCoords.X = (float)x / 30.0f;
                    textureCoords.Y = (float)y / 30.0f;
                    vertices.Add(new VertexPositionNormalTexture(position, new Vector3(), textureCoords));//AddSquare(x, y);
                }
            Vertices = vertices.ToArray();

            int i = 0;
            Indices = new int[(width - 1) * (width - 1) * 6];
            for (int x = 0; x < width - 1; x++)
                for (int y = 0; y < width - 1; y++)
                {
                    Indices[i++] = x + y * width;
                    Indices[i++] = x + 1 + (y + 1) * width;
                    Indices[i++] = x + 1 + y * width;

                    Indices[i++] = x + y * width;
                    Indices[i++] = x + (y + 1) * width;
                    Indices[i++] = x + 1 + (y + 1) * width;
                }
            CalculateNormals();
            CopyToBuffer();
        }

        public float GetValue(int x, int y)
        {
            int width = (int)Math.Sqrt(Height.Length);
            return Height[(x + width / 2) + (y + width / 2) * width];
        }

        private void CalculateNormals()
        {
            for (int i = 0; i < Vertices.Length; i++)
                Vertices[i].Normal = new Vector3(0, 0, 0);
            for (int i = 0; i < Indices.Length / 3; i++)
            {
                int index1 = Indices[i * 3];
                int index2 = Indices[i * 3 + 1];
                int index3 = Indices[i * 3 + 2];

                Vector3 side1 = Vertices[index1].Position - Vertices[index3].Position;
                Vector3 side2 = Vertices[index1].Position - Vertices[index2].Position;
                Vector3 normal = Vector3.Cross(side1, side2);

                Vertices[index1].Normal += normal;
                Vertices[index2].Normal += normal;
                Vertices[index3].Normal += normal;
            }
            for (int i = 0; i < Vertices.Length; i++)
                Vertices[i].Normal.Normalize();
        }
    }
}

