using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace RTS
{
    public class ModelLoader
    {
        static List<Mesh> meshes;
        static List<Vector3> positions;
        static List<Vector3> normals;
        static List<Vector2> textureCoords;
        static Mesh currentMesh;


        public static List<Mesh> LoadModel(String fileName)
        {
            meshes = new List<Mesh>();
            positions = new List<Vector3>();
            normals = new List<Vector3>();
            textureCoords = new List<Vector2>();
            String model = File.ReadAllText(fileName);
            String[] lines = model.Split('\n');

            foreach (String line in lines)
            {
                String[] tokens = line.Split(' ');
                switch (tokens[0])
                {
                    case "o":
                        currentMesh = new Mesh(Util.TexturePath + @"\barrel_texture.png");
                        meshes.Add(currentMesh);
                        break;
                    case "v":
                        float x = float.Parse(tokens[1]);
                        float y = float.Parse(tokens[2]);
                        float z = float.Parse(tokens[3]);
                        Vector3 v = new Vector3(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3]));
                        positions.Add(v);
                        break;
                    case "vn":
                        normals.Add(new Vector3(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])));
                        break;
                    case "vt":
                        textureCoords.Add(new Vector2(float.Parse(tokens[1]), float.Parse(tokens[2])));
                        break;
                    case "f":
                        currentMesh.Faces.Add(ReadFace(tokens));
                        break;
                }
            }
            return meshes;
        }

        public static Face ReadFace(String[] tokens)
        {
            Face face = new Face();
            for (int i = 1; i < tokens.Length; i++)
            {
                String[] tok = tokens[i].Split('/');
                int index = int.Parse(tok[0]) - 1;

                int textureCoord = int.Parse(tok[1]) - 1;
                int normal = int.Parse(tok[2]) - 1;

                VertexPositionNormalTexture v = new VertexPositionNormalTexture();
                v.Position = positions[index];
                v.Normal = normals[normal];
                v.TextureCoordinate = textureCoords[textureCoord];
                face.Vertices.Add(v);
            }
            return face;
        }
    }
}
