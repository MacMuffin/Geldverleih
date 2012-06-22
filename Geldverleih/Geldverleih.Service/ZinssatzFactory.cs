using System.Collections.Generic;

namespace Geldverleih.Service
{
    public class ZinssatzFactory : IZinssatzFactory
    {
        public IList<decimal> GetAlleZinssaetze()
        {
            return new List<decimal>
                       {
                           3.2m,
                           5.9m,
                           6.4m
                       };
        }
    }
}