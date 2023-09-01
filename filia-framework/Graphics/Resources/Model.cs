using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiliaFramework.Graphics.Resources;

namespace FiliaFramework.Graphics
{
    public class Model
    {
        public List<Texture> ModelTextures = new List<Texture>();
        public List<Material> ModelMaterials = new List<Material>();
        public string Directory { get; protected set; } = string.Empty;
        public List<Mesh> ModelMeshes { get; protected set; } = new List<Mesh>();

        public Model(string path)
        {
            Directory = path;
        }

    }
}
