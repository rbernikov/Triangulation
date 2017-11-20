namespace Triangulation.Geometry
{
    public class Edge
    {
        public Edge(Vertex v0, Vertex v1)
        {
            V0 = v0;
            V1 = v1;
        }

        public Vertex V0 { get; }

        public Vertex V1 { get; }

        public static bool operator ==(Edge left, Edge right)
        {
            return left.V0 == right.V0 && left.V1 == right.V1 || left.V0 == right.V1 && left.V1 == right.V0;
        }

        public static bool operator !=(Edge left, Edge right)
        {
            return !(left == right);
        }

        protected bool Equals(Edge other)
        {
            return V0 == other.V0 && V1 == other.V1 || V0 == other.V1 && V1 == other.V0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Edge)obj);
        }

        public override int GetHashCode()
        {
            return V0.GetHashCode() ^ V1.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(V0)}: {V0}, {nameof(V1)}: {V1}";
        }
    }
}