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
        string fileName;
        private IAppMenuItem pushItem;
        public override void InitializeTaxPrepAddin()
        {
            /**/
            FEvents = (IAppClientFileEventsService)_appInstance;
            

            FEvents.AfterClientFileSave = new TxpAddinLibrary.Handlers.ClientFile.AfterSaveHandler(
               (aFilename) =>
               {
                   fileName = aFilename;
                   DoCommit();
               });
            /**/
            var appMenuService = (IAppMenuService)_appInstance;
            if (appMenuService != null)
            {
                var subMenu = appMenuService.AddRootMenu("Empty add-in");
                subMenu.Visible = true;
                

                var pushItem = subMenu.AddItem("Push", false);
                pushItem.ClickHandler = new TxpAddinLibrary.Handlers.AppNotifyHandler(DoPush);
                pushItem.Visible = true;
                pushItem.Enabled = false;

                var updateItem = subMenu.AddItem("Update", false);
                updateItem.ClickHandler = new TxpAddinLibrary.Handlers.AppNotifyHandler(Update);
                updateItem.Visible = true;
                updateItem.Enabled = true;

                var checkoutItem = subMenu.AddItem("Checkout", false);
                checkoutItem.ClickHandler = new TxpAddinLibrary.Handlers.AppNotifyHandler(Checkout);
                checkoutItem.Visible = true;
                checkoutItem.Enabled = true;

                var branchItem = subMenu.AddItem("Branch", false);
                branchItem.ClickHandler = new TxpAddinLibrary.Handlers.AppNotifyHandler(Branch);
                branchItem.Visible = true;
                branchItem.Enabled = true;

                var aboutItem = subMenu.AddItem("About us", false);
                aboutItem.ClickHandler = new TxpAddinLibrary.Handlers.AppNotifyHandler(aboutUs);
                aboutItem.Visible = true;
                aboutItem.Enabled = true;
            }
        }

        private void DoCommit()
        {
            string test = this.fileName;
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

        private void aboutUs() 
        {
            /*Make ascii Viking and push it to Popup window*/
            string asciiViking = "";
            var app = (IAppTaxApplicationService)_appInstance;
            app.ShowMessageString("Viking!", asciiViking);
        }

        public IAppClientFileEventsService FEvents { get; set; }
    }
}
