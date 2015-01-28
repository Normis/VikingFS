using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers
{
    public class AppIdleHandler : IAddinIdleHandler
    {
        public delegate void ExecuteDelegate(out bool aProcessed);
        private readonly ExecuteDelegate _onExecute;

        #region IAddinIdleHandler
        public void Execute(out bool aProcessed)
        {
            if (_onExecute != null)
            {
                _onExecute(out aProcessed);
            }
            else
            {
                aProcessed = true;
            }
        }
        #endregion

        public AppIdleHandler(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
