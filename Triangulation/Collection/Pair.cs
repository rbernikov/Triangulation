namespace Triangulation.Collection
{
    public struct Pair<T1, T2>
    {
        public Pair(T1 key, T2 value)
        {
            Key = key;
            Value = value;
        }

        public T1 Key { get; }

        public T2 Value { get; }
    }
}