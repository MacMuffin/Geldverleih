using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geldverleih.Repository;
using Geldverleih.Repository.interfaces;
using Geldverleih.Service;
using Geldverleih.Service.interfaces;
using Geldverleih.UI.Logik;
using Microsoft.Practices.Unity;

namespace Geldverleih.Unity
{
    public class GeldverleihUnityContainer
    {
        private IUnityContainer _unityContainer;
            
        public IUnityContainer UnityContainer
        {
            get
            {
                if (_unityContainer == null)
                {
                    _unityContainer = CreateUnityContainer();
                }
                    

                return _unityContainer;
            }
        }

        private IUnityContainer CreateUnityContainer()
        {
            IUnityContainer unityContainer = new UnityContainer();

            unityContainer.RegisterType<IAusleihRepository, AusleihRepository>();
            unityContainer.RegisterType<IRueckzahlReppository, RueckzahlRepository>();
            unityContainer.RegisterType<IBankService, BankService>();
            unityContainer.RegisterType<IKundenRepository, KundenRepository>();
            unityContainer.RegisterType<IKundenService, KundenService>();
            unityContainer.RegisterType<IAusUndRueckzahlvorgangFactory, AusUndRueckzahlvorgangFactory>();
            unityContainer.RegisterType<IZinsRechner, TagesZinsRechner>();
            unityContainer.RegisterType<IZinssatzFactory, ZinssatzFactory>();

            return unityContainer;
        }
    }
}
