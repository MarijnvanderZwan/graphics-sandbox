using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RTS
{
    public class Map2D : GameObjectTextured
    {
        public int[,] obstacles;
        int Width, Height;

        public Map2D(Texture2D heightMap)
        {
            Width = heightMap.Width;
            Height = heightMap.Height;

            Texture = Util.TextureFromFile(Util.TexturePath +@"\maze.png");
            Color[] heightMapData = new Color[Width * Height];
            obstacles = new int[Width, Height];
            heightMap.GetData<Color>(heightMapData);

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    obstacles[x, y] = heightMapData[x + y * Width].R > 0 ? 0 : 1;

            Vertices = new VertexPositionNormalTexture[4];

            Vertices[0] = new VertexPositionNormalTexture(new Vector3(),                 new Vector3(0, 1, 0), new Vector2());
            Vertices[1] = new VertexPositionNormalTexture(new Vector3(Width, 0, 0),      new Vector3(0, 1, 0), new Vector2(1, 0));
            Vertices[2] = new VertexPositionNormalTexture(new Vector3(Width, 0, Height), new Vector3(0, 1, 0), new Vector2(1, 1));
            Vertices[3] = new VertexPositionNormalTexture(new Vector3(    0, 0, Height), new Vector3(0, 1, 0), new Vector2(0, 1));

            Indices = new int[] { 0, 1, 2, 0, 2, 3 };

            CopyToBuffer();
        }
    }
}
