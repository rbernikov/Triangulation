using System;
using System.Collections.Generic;
using System.Linq;

namespace Triangulation.Tree
{
    public class TreeNode
    {
        private readonly List<TreeNode> _children;

        public TreeNode(string id)
        {
            Id = id;
            _children = new List<TreeNode>();
        }

        public string Id { get; }

        public TreeNode Parent { get; private set; }

        public IReadOnlyCollection<TreeNode> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public TreeNode this[int index]
        {
            get
            {
                Preconditions.CheckOutOfRange(index, 0, _children.Count, "index");

                return _children[index];
            }
        }

        public TreeNode this[string id]
        {
            get
            {
                Preconditions.CheckNotNullOrEmpty(id, "id");

                return _children.FirstOrDefault(x => x.Id == id);
            }
        }

        public TreeNode AddChild(string id)
        {
            var item = new TreeNode(id) { Parent = this };
            _children.Add(item);
            return item;
        }

        public bool RemoveChild(TreeNode item)
        {
            return _children.Remove(item);
        }

        public void Traverse(Action<TreeNode> action)
        {
            action(this);
            foreach (var child in _children)
                child.Traverse(action);
        }
    }
}