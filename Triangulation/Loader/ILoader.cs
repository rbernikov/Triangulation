using Triangulation.Tree;

namespace Triangulation.Loader
{
    public interface ILoader
    {
        Node Load(string filename);
    }
}