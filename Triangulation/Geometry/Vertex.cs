namespace Triangulation.Geometry
{
    public class Vertex
    {
        public Vertex(double x, double y)
            : this(x, y, -1)
        {
        }

        public Vertex(double x, double y, int id)
        {
            X = x;
            Y = y;

            Id = id;
        }

        public double X { get; }

        public double Y { get; }

        public int Id { get; }

        public static bool operator ==(Vertex left, Vertex right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Vertex left, Vertex right)
        {
            return !(left == right);
        }

        protected bool Equals(Vertex other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Vertex)obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Id)}: {Id}";
        }
    }
}