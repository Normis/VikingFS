using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.ModuleManager
{
    public class NotifyHandler : IAddinModuleManagerNotifyHandler
    {
        public delegate void ExecuteDelegate(IAppModule aModule);
        private readonly ExecuteDelegate _onExecute;

        #region IAddinModuleManagerNotifyHandler
        public void Execute(IAppModule aModule)
        {
            if (_onExecute != null)
                _onExecute(aModule);
        }
        #endregion

        public NotifyHandler(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
