using Geldverleih.Repository;
using Geldverleih.Repository.interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geldverleih.Tests
{
    [TestClass]
    public class KundenrepositoryTest
    {
        [TestMethod]
        public void KunderepositoryKannErstelltWerden()
        {
            IKundenRepository kundenRepository = new KundenRepository();

            Assert.IsNotNull(kundenRepository);
        }
    }
}