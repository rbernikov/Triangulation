using System;

namespace Triangulation.Union
{
    public class UnionFactory
    {
        public static AUnion Create(int method)
        {
            if (method < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(method), @"method must be > 0");
            }

            AUnion result;
            switch (method)
            {
                case 0:
                    result = new WetUnion();
                    break;
                case 1:
                    // TODO: Add your code here
                default:
                    result = null;
                    break;
            }

            return result;
        }
    }
}