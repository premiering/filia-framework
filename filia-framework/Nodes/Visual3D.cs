using FiliaFramework.Renderer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Nodes
{
    public class Visual3D : Node3D
    {
        public Renderer.Common.Renderer? Renderer { protected get; set; }
        //TODO: specify the fbo/camera to use, etc
        public Dictionary<string, object> RendererData { get; set; } = new Dictionary<string, object>();

        public override void Load()
        {
            base.Load();

            Renderer = Game!.Renderer;
            //Renderer.LoadVisual3DNode(this);
        }
    }
}
