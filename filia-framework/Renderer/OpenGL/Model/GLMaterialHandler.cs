using FiliaFramework.Graphics.Resources;
using FiliaFramework.Renderer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.OpenGL.Model
{
    public class GLMaterialHandler : MaterialHandler
    {
        private Dictionary<RenderId, GLMaterial> _materials = new Dictionary<RenderId, GLMaterial>();
        private GLTextureHandler _textureHandler;

        public GLMaterialHandler(GLTextureHandler textureHandler)
        {
            _textureHandler = textureHandler;
        }

        public override RenderId UploadMaterial(Material material)
        {
            var rid = RenderId.CreateRID();

            RenderId diffuseRid = _textureHandler.GetDefaultWhiteTexture();
            if (material.DiffuseTexture != null)
                diffuseRid = _textureHandler.UploadTexture(material.DiffuseTexture);

            var glMat = new GLMaterial(material, diffuseRid);
            _materials.Add(rid, glMat);

            return rid;
        }

        public override void DisposeMaterial(RenderId rid)
        {
            RenderId.DisposeRID(rid);
            _materials.Remove(rid);
        }

        public override void UpdateMaterial(RenderId rid, Material material)
        {
            var glMat = _materials[rid];
            glMat.Material = material;
        }

        public GLMaterial GetMaterial(RenderId rid)
        {
            return _materials[rid];
        }
    }
}
