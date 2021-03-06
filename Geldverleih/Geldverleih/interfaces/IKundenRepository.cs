﻿using System.Collections.Generic;
using Geldverleih.Domain;

namespace Geldverleih.Repository.interfaces
{
    public interface IKundenRepository
    {
        void KundenAnlegen(Kunde kunde);
        IList<Kunde> GetAlleKunden();
    }
}
