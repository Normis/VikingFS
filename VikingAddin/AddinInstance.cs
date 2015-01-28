using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxprepAddinAPI;
using TxpAddinLibrary;
using VikingFS;

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

        private IAppMenuItem pushItem;
        public override void InitializeTaxPrepAddin()
        {
            var appMenuService = (IAppMenuService)_appInstance;
            if (appMenuService != null)
            {
                var subMenu = appMenuService.AddRootMenu("Empty add-in");
                subMenu.Visible = true;
                
                var commitItem = subMenu.AddItem("Commit", false);
                commitItem.ClickHandler = new TxpAddinLibrary.Handlers.AppNotifyHandler(DoCommit);
                commitItem.Visible = true;
                commitItem.Enabled = true;

                pushItem = subMenu.AddItem("Push", false);
                pushItem.ClickHandler = new TxpAddinLibrary.Handlers.AppNotifyHandler(DoPush);
                pushItem.Visible = true;
                pushItem.Enabled = false;
            }
        }

        private void DoCommit()
        {
            pushItem.Enabled = true;
        }

        private void DoPush()
        {
            pushItem.Enabled = false;
        }

        private void Update()
        {

        }

        private void Checkout()
        {

        }

        private void Branch()
        {

        }
    }
}
