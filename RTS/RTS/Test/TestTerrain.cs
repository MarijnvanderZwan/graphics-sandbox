using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    public class TestTerrain : Terrain
    {
        public TestTerrain() : base()
        {
            Texture = Util.TextureFromFile(Util.TexturePath + @"\grass_texture.png");
           
            Texture2D heightMap = Util.TextureFromFile(Util.TexturePath + @"\heightmap.png");
            VerticesFromHeightMap(heightMap);
            
            TerrainToVertices();
        }
    }
}
