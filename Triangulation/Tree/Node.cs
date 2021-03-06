﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Triangulation.Geometry;

namespace Triangulation.Tree
{
    public class Node
    {
        private readonly List<Node> _children;

        public Node(string id)
        {
            Id = id;
            Vertices = new List<Vertex>();
            _children = new List<Node>();
        }

        public string Id { get; }

        public int Label { get; set; }

        public int Depth
        {
            get
            {
                if (Parent == null) return 0;

                return Parent.Depth + 1;
            }
        }

        public bool HasChildren
        {
            get { return _children.Count > 0; }
        }

        public Node Parent { get; private set; }

        public List<Point> Boundary { get; set; }

        public IReadOnlyCollection<Node> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public List<Vertex> Vertices { get; set; }

        public Node this[int index]
        {
            get
            {
                Preconditions.CheckOutOfRange(index, 0, _children.Count, "index");

                return _children[index];
            }
        }

        public Node this[string id]
        {
            get
            {
                Preconditions.CheckNotNullOrEmpty(id, "id");

                return _children.FirstOrDefault(x => x.Id == id);
            }
        }

        public Node AddChild(string id)
        {
            var item = new Node(id) { Parent = this };
            _children.Add(item);
            return item;
        }

        public bool RemoveChild(Node item)
        {
            return _children.Remove(item);
        }

        public void ClearChildren()
        {
            foreach (var child in _children)
            {
                child.Vertices.Clear();
                child.ClearChildren();
            }

            _children.Clear();
        }

        public void AddVertex(Vertex item)
        {
            Preconditions.CheckNotNull(item, nameof(item));

            Vertices.Add(item);
        }

        public void Traverse(Action<Node> action)
        {
            action(this);
            foreach (var child in _children)
                child.Traverse(action);
        }

        public void Clear()
        {
            ClearChildren();
            Vertices.Clear();
        }
    }
}