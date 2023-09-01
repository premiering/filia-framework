using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.Common
{
    public struct RenderId
    {
        private static UInt128 _idCounter = 0;
        private static List<RenderId> _newAvailable = new List<RenderId>();//Over-optimizing?

        public static RenderId CreateRID()
        {
            RenderId rid;

            if (_newAvailable.Count > 0)
            {
                rid = _newAvailable[0];
                _newAvailable.RemoveAt(0);
            }
            else
            {
                rid = new RenderId(_idCounter);
                _idCounter++;
            }
            return rid;
        }

        public static void DisposeRID(RenderId rid)
        {
            _newAvailable.Add(rid);
        }

        public UInt128 Id;

        public RenderId(UInt128 renderId)
        {
            Id = renderId;
        }
    }
}
