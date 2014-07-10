using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace RTS
{
    public class Terrain : GameObjectTextured
    {
        public float[] TerrainMap = new float[0];
        public int Width, Height;

        public Terrain() : base()
        {
        }

        public void InitializeTerrain(Texture2D heightMap)
        {
            Color[] heightMapColors = new Color[Width * Height];
            heightMap.GetData(heightMapColors);

            TerrainMap = new float[Width * Height];
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    TerrainMap[x + y * Width] = heightMapColors[x + y * Width].R / 5.0f;
        }

        public void TerrainToVertices()
        {
            List<VertexPositionNormalTexture> vertices = new List<VertexPositionNormalTexture>();
            Vector3 offset = new Vector3(-Width / 2, 0, -Height / 2);
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    Vector3 position = new Vector3(x, TerrainMap[x + y * Width], y) + offset;
                    Vector2 textureCoords = new Vector2(x, y) / 30.0f;
                    vertices.Add(new VertexPositionNormalTexture(position, new Vector3(), textureCoords));
                }

            Vertices = vertices.ToArray();

            CreateIndices();
           
            CalculateNormals();
            CopyToBuffer();
        }

        private void CreateIndices()
        {
            int i = 0;
            Indices = new int[(Width - 1) * (Height - 1) * 6];
            for (int x = 0; x < Width - 1; x++)
                for (int y = 0; y < Height - 1; y++)
                {
                    Indices[i++] = x + y * Width;
                    Indices[i++] = x + 1 + (y + 1) * Width;
                    Indices[i++] = x + 1 + y * Width;

                    Indices[i++] = x + y * Width;
                    Indices[i++] = x + (y + 1) * Width;
                    Indices[i++] = x + 1 + (y + 1) * Width;
                }
        }

        private void CalculateNormals()
        {
            for (int i = 0; i < Indices.Length / 3; i++)
                CalculateNormalsForFace(i);

            for (int i = 0; i < Vertices.Length; i++)
                Vertices[i].Normal.Normalize();
        }

        private void CalculateNormalsForFace(int i)
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
    }
}

