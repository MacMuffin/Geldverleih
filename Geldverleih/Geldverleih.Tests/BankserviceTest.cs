using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Geldverleih.Domain;
using Geldverleih.Repository;
using Geldverleih.Repository.interfaces;
using Geldverleih.Service;
using Geldverleih.Service.interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Geldverleih.Tests
{
    [TestClass]
    public class BankserviceTest
    {

        MockRepository _mockRepository = new MockRepository();

        [TestMethod]
        public void BankserviceKannErstelltWerden()
        {
            IAusleihRepository ausleihRepository = new AusleihRepository();
            IBankService bankmanager = new BankService(ausleihRepository);

            Assert.IsNotNull(bankmanager);
        }

        [TestMethod]
        public void AusleihvorgangWirdInsRepositoryGeschrieben()
        {

            IAusleihRepository ausleihRepository = _mockRepository.StrictMock<IAusleihRepository>();
            IBankService bankmanager = _mockRepository.StrictMock<IBankService>();

            Kunde kunde = new Kunde();
            VerleihKondition verleihKondition = new VerleihKondition();

            using (_mockRepository.Record())
            {
                Expect.Call(() => bankmanager.GeldAusleihen(kunde, verleihKondition));
                Expect.Call(() => ausleihRepository.GeldAnKundenAusleihen(kunde, verleihKondition));
            }

            _mockRepository.ReplayAll();

            bankmanager.GeldAusleihen(kunde, verleihKondition);

            Assert.IsNotNull(bankmanager);
        }
    }
}
