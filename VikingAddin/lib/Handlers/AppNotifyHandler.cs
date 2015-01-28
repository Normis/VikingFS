using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers
{
    public class AppNotifyHandler : IAddinNotifyHandler
    {
        public delegate void ExecuteDelegate();
        private readonly ExecuteDelegate _onExecute;

        #region IAddinNotifyHandler
        public void Execute()
        {
            if (_onExecute != null)
                _onExecute();
        }
        #endregion

        public AppNotifyHandler(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
