using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Input
{
    public interface IInputManager
    {
        //Keyboard API
        public bool IsCharDown(char c);
        public bool IsCharUp(char c);
        public bool IsKeyDown(Key key);
        public bool IsKeyUp(Key key);

        //Mouse API
        public bool IsMouseDown(int i);
        public bool IsMouseUp(int i);
        public int GetMouseX();
        public int GetMouseY();
        public void CaptureCursor(bool captured);
    }
}
