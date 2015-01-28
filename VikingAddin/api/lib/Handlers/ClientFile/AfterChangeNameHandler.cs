using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.ClientFile
{
    public class AfterChangeNameHandler : IAddinAfterClientFileNameChangeHandler
    {
        public delegate void ExecuteDelegate(string aFilename);
        private readonly ExecuteDelegate _onExecute;

        #region IAddinAfterClientFileNameChangeHandler

        public void Execute(string aFileName)
        {
            if (_onExecute != null)
            {
                _onExecute(aFileName);
            }
        }

        #endregion

        public AfterChangeNameHandler(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
