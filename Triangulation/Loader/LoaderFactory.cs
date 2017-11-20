using System;

namespace Triangulation.Loader
{
    public class LoaderFactory
    {
        public const int Json = 0;
        public const int Excel = 1;

        public static ILoader Create(int loader)
        {
            if (loader < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(loader), @"loader must be > 0");
            }

            ILoader result;
            switch (loader)
            {
                case 0:
                    result = new JsonLoader();
                    break;
                case 1:
                    result = new ExcelLoader();
                    break;
                default:
                    result = null;
                    break;
            }

            return result;
        }
    }
}