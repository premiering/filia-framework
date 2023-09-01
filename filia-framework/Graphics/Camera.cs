using FiliaFramework.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Graphics
{
    public class Camera
    {
        public static Vector3 CameraUp = Vector3.UnitY;

        public Vector3 CameraPosition = new Vector3(0.0f, 0.0f, 3.0f);
        public Vector3 CameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        //public Vector3 CameraUp  = Vector3.UnitY;
        public Vector3 CameraDirection = Vector3.Zero;
        public float CameraYaw = -90f;
        public float CameraPitch = 0f;
        public float CameraFov = 80f;
        public float NearPlaneDist = 0.1f;
        public float FarPlaneDist = 1000000f;
    }
}
