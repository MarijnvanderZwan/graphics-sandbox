using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.IO;

namespace RTS
{
    public class Util
    {
        public Util()
        {

        }

        public static Texture2D TextureFromFile(String path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            Texture2D texture = Texture2D.FromStream(Game1.Instance.GraphicsDevice, file);
            file.Close();
            return texture;
        }

        public static String CodeRoot
        {
            get { return @"C:\Users\Marijn\Documents\Programming\VS Workspace\RTS\RTS\RTS"; }
        }

        public static String ModelPath
        {
            get { return CodeRoot + @"\Models"; }
        }

        public static String TexturePath
        {
            get { return CodeRoot + @"\Textures"; }
        }

    }
}



/*
            Bitmap bitmap = new Bitmap(path);
            int width = bitmap.Width;
            int height = bitmap.Height;
            Int32[] colors = new Int32[width * height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Color c = bitmap.GetPixel(x, y);
                    colors[width * y + x] = Color.FromArgb(255, c.B, c.G, c.R).ToArgb();
                }

            Texture2D texture = new Texture2D(Game1.Instance.GraphicsDevice, width, height);
            texture.SetData<Int32>(colors);

            return texture;*/
