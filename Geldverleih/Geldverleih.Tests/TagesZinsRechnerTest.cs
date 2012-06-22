using System;
using Geldverleih.Service;
using Geldverleih.Service.interfaces;
using Geldverleih.UI.Logik;
using Geldverleih.UI.presenters;
using Geldverleih.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Geldverleih.Tests
{
    [TestClass]
    public class TagesZinsRechnerTest
    {
        [TestMethod]
        public void TagesZinsRechnerKannErstelltWerden()
        {
            GeldverleihUnityContainer geldverleihUnityContainer = new GeldverleihUnityContainer();

            IBankService bankservice = geldverleihUnityContainer.UnityContainer.Resolve<IBankService>();
            IZinsRechner zinsRechner = new TagesZinsRechner(bankservice);

            Assert.IsNotNull(zinsRechner);
        }
    }
}