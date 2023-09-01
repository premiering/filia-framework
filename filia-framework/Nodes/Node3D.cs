using FiliaFramework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Nodes
{
    public class Node3D : Node
    {
        //The resulting transform is GlobalTransform + LocalTransform
        //So global transform could be seen as the transform of the parenting node
        //And the local transform relative to the parent one
        public virtual Transform GlobalTransform { get; set; } = new Transform();
        public virtual Transform LocalTransform { get; set; } = new Transform();

        public void SetPositionLocal(Vector3 pos)
        {
            LocalTransform.Position = pos;
        }

        public void SetScaleLocal(Vector3 scale)
        {
            LocalTransform.Scale = scale;
        }

        public void SetRotationLocal(Quaternion rot)
        {
            LocalTransform.Rotation = rot;
        }

        public void SetPositionGlobal(Vector3 pos)
        {
            GlobalTransform.Position = pos;
        }

        public void SetScaleGlobal(Vector3 scale)
        {
            GlobalTransform.Scale = scale;
        }

        public void SetRotationGlobal(Quaternion rot)
        {
            GlobalTransform.Rotation = rot;
        }

        public void ResetGlobalTransform()
        {
            GlobalTransform = new Transform();            
        }

        public void ResetLocalTransform()
        {
            LocalTransform = new Transform();
        }
    }
}
