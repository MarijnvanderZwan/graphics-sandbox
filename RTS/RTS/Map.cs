using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RTS
{
    public class Map : GameObjectTextured
    {
        public int[,] obstacles;
        int Width, Height;

        public Map(Texture2D heightMap)
        {
            Width = heightMap.Width;
            Height = heightMap.Height;
            Texture = Util.TextureFromFile(Util.TexturePath +@"\binary.png");
            Color[] heightMapData = new Color[Width * Height];
            obstacles = new int[Width, Height];
            heightMap.GetData<Color>(heightMapData);

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    obstacles[x, y] = heightMapData[x + y * Width].R > 0 ? 0 : 1;
                }

            Vertices = new VertexPositionNormalTexture[Width * Height];

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    Vector3 position = new Vector3(x, 0, y);
                    Vector2 textureCoords = new Vector2(obstacles[x, y], 0);
                    Vertices[x + y * Width] = new VertexPositionNormalTexture(position, new Vector3(), textureCoords);
                }

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
