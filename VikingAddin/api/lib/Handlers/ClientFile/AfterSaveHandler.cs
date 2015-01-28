using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.ClientFile
{
    public class AfterSaveHandler : IAddinAfterClientFileSaveHandler
    {
        public delegate void ExecuteDelegate(string aFileName);
        private readonly ExecuteDelegate _onExecute;

        #region IAddinAfterClientFileSavHandler

        public void Execute(string aFileName)
        {
            if (_onExecute != null)
                _onExecute(aFileName);
        }

        #endregion

        public AfterSaveHandler(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
