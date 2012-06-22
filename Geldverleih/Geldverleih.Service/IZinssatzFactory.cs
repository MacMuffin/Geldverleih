using System;
using System.Collections.Generic;

namespace Geldverleih.Service
{
    public interface IZinssatzFactory
    {
        IList<decimal> GetAlleZinssaetze();
    }
}