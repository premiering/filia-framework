using FiliaFramework.Graphics;
using FiliaFramework.Renderer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.OpenGL.Model
{
    public class GLTextureHandler : TextureHandler
    {
        private Dictionary<RenderId, GLTexture> _textures = new Dictionary<RenderId, GLTexture>();

        private RenderId _defaultWhiteTex;

        public GLTextureHandler()
        {
            _defaultWhiteTex = UploadTexture(new Graphics.Resources.Texture(1, 1, new byte[] { 255, 255, 255, 255 }));
        }

        public override RenderId UploadTexture(Graphics.Resources.Texture texture)
        {
            var rid = RenderId.CreateRID();

            var tex = new GLTexture(texture);
            _textures.Add(rid, tex);

            return rid;
        }

        public override void DisposeTexture(RenderId rid)
        {
            RenderId.DisposeRID(rid);

            _textures.Remove(rid);
        }

        public override RenderId GetDefaultWhiteTexture()
        {
            return _defaultWhiteTex;
        }

        public GLTexture GetTexture(RenderId rid) { return _textures[rid]; }
    }
}
