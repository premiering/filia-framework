using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Nodes
{
    public class Node
    {
        protected FiliaGame Game { get; } = FiliaGame.Instance;

        private Node? _parent;
        private readonly List<Node> _children = new List<Node>();

        public bool Loaded { get; private set; }

        public Node() : this(null)
        {
        }

        public Node(Node? parent)
        {
            _parent = parent;
        }

        public virtual void Load()
        {
            //LoadChildrenRecursive(this);

            Loaded = true;
        }

        public virtual void OnUpdate(float deltaTime)
        {
        }

        public virtual void OnFrame(float deltaTime)
        {
        }

        public virtual void Unload()
        {
        }

        private void LoadChildrenRecursive(Node node)
        {
            node.Load();
            foreach (Node child in _children)
            {
                child.LoadChildrenRecursive(node);
            }
        }

        public Node GetParent()
        {
            return _parent;
        }

        public List<Node> GetChildren()
        {
            return _children;
        }

        public void AddChild(Node child)
        {
            _children.Add(child);
            child.Load();
        }

        public void RemoveChild(Node child)
        {
            _children.Remove(child);
            child.Unload();
        }
    }
}
