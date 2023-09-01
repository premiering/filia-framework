using FiliaFramework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Nodes
{
    public class Camera3D : Node3D
    {
        public Camera Camera { get; set; } = new Camera();
        public bool IsActive { get; set; } = true;
    }
}
