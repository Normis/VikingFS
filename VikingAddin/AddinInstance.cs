using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxprepAddinAPI;
using TxpAddinLibrary;

namespace EmptyAddin
{
    [AddinInstance]
    public class AddinInstance : AddinInstanceBase
    {
        public override void ReleaseApp()
        {
            base.ReleaseApp();
            //some finalization code
        }

        public AddinInstance()
            : base(new Guid("26A2ECE8-ED75-47B9-8797-32B3C0D227A8"), "Empty hello world add-in example via ProxyAddin", 
            "1.0.0.0")
        { 
            //some class initizalition code
        }

        public override void InitializeTaxPrepAddin()
        {
            var appMenuService = (IAppMenuService)_appInstance;
            if (appMenuService != null)
            {
                var subMenu = appMenuService.AddRootMenu("Empty add-in");
                subMenu.Visible = true;

                var item = subMenu.AddItem("Hello world", false);
                item.ClickHandler = new TxpAddinLibrary.Handlers.AppNotifyHandler(DoHelloWorld);
                item.Visible = true;
                item.Enabled = true;
            }
        }

        private void DoHelloWorld()
        {
            var app = (IAppTaxApplicationService)_appInstance;
            app.ShowMessageString("Test message", "Hello world!");
        }
    }
}
