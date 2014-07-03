using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.IO;

namespace RTS
{
    public class TestTerrain : Terrain
    {
        public TestTerrain() : base()
        {
            texture = Util.TextureFromBitmap(Util.TexturePath + @"\grass_texture.png");
           
            Texture2D heightMap = Util.TextureFromBitmap(Util.TexturePath + @"\heightmap.png");
            VerticesFromHeightMap(heightMap);
            
            TerrainToVertices();
        }
    }
}
