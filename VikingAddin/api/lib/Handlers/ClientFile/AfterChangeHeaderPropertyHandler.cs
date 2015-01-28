using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.ClientFile
{
    public class AfterChangeHeaderPropertyHandler : IAddinAfterChangeClientFileHeaderPropertyHandler
    {
        public delegate void ExecuteDelegate(string aFilename);
        private readonly ExecuteDelegate _onExecute;

        #region

        public void Execute(string aClientFileHeaderKey)
        {
            if (_onExecute != null)
            {
                _onExecute(aClientFileHeaderKey);
            }
        }

        #endregion

        public AfterChangeHeaderPropertyHandler(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
