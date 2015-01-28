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
        string filePath;
        FieldComparer aComparer;
        Boolean ComparerInit = false;
        private IAppMenuItem pushItem;
        public override void InitializeTaxPrepAddin()
        {
            /**/
            FEvents = (IAppClientFileEventsService)_appInstance;
            

            FEvents.AfterClientFileSave = new TxpAddinLibrary.Handlers.ClientFile.AfterSaveHandler(
               (aFilename) =>
               {
                   fileName = aFilename;
                   int id = fileName.LastIndexOf(@"\");
                   filePath = fileName.Substring(0,id+1);
                   fileName = fileName.Substring(id + 1);
                   initComparer();
                   DoCommit();
               });
            /**/
            var appMenuService = (IAppMenuService)_appInstance;
            if (appMenuService != null)
            {
                var subMenu = appMenuService.AddRootMenu("VikingFS");
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

                var aboutItem = subMenu.AddItem("About VikingFS", false);
                aboutItem.ClickHandler = new TxpAddinLibrary.Handlers.AppNotifyHandler(aboutUs);
                aboutItem.Visible = true;
                aboutItem.Enabled = true;
            }
        }

        private void DoCommit()
        {
            var app = (IAppTaxApplicationService)_appInstance;
            app.ShowMessageString(this.filePath, this.fileName);
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
            string asciiViking =
@"                   ~.
            Ya...___|__..ab.     .   .
             Y88b  \88b  \88b   (     )
              Y88b  :88b  :88b   `.oo'
              :888  |888  |888  ( (`-'
     .---.    d88P  ;88P  ;88P   `.`.
    / .-._)  d8P-'''|''''-Y8P      `.`.
   ( (`._) .-.  .-. |.-.  .-.  .-.   ) )
    \ `---( O )( O )( O )( O )( O )-' /
     `.    `-'  `-'  `-'  `-'  `-'  .'
       `---------------------------'";
            var app = (IAppTaxApplicationService)_appInstance;
            app.ShowMessageString("About Viking!", asciiViking);
        }

        private void initComparer() 
        {
            if (!ComparerInit) 
            {
                aComparer = new VikingFS.FieldComparer(comFilePath, middlewareFilePath, this.filePath, this.fileName);
                ComparerInit = true;
            }
            
        }
        public IAppClientFileEventsService FEvents { get; set; }
    }
}
