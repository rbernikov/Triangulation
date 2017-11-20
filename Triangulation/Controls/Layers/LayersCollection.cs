using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Triangulation.Controls.Layers
{
    public class LayersCollection : IList<ILayer>
    {
        private readonly Map _map;
        private readonly IList<ILayer> _layers;

        internal LayersCollection(Map map)
        {
            _map = map;
            _layers = new List<ILayer>();
        }

        public IEnumerator<ILayer> GetEnumerator()
        {
            return _layers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ILayer item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            item.Parent = _map;

            _layers.Add(item);
            _map.Invalidate();
        }

        public void Clear()
        {
            if (_layers.Count > 0)
            {
                _layers.Clear();
                _map.Invalidate();
            }
        }

        public bool Contains(ILayer item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return _layers.Contains(item);
        }

        public void CopyTo(ILayer[] array, int arrayIndex)
        {
            _layers.CopyTo(array, arrayIndex);
        }

        public bool Remove(ILayer item)
        {
            var remove = _layers.Remove(item);
            if (remove)
                _map.Invalidate();

            return remove;
        }

        public int Count => _layers.Count;

        public bool IsReadOnly => false;

        public int IndexOf(ILayer item)
        {
            return _layers.IndexOf(item);
        }

        public void Insert(int index, ILayer item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            item.Parent = _map;

            _layers.Insert(index, item);
            _map.Invalidate();
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            _layers.RemoveAt(index);
            _map.Invalidate();
        }

        public ILayer this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                return _layers[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                _layers[index] = value;
                _map.Invalidate();
            }
        }

        public ILayer this[string name]
        {
            get { return _layers.FirstOrDefault(layer => layer.Name == name); }
        }
    }
}