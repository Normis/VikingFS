using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.ClientFile
{
    public class BeforeChangeNameHandler : IAddinBeforeClientFileNameChangeHandler
    {
        public delegate void ExecuteDelegate(string aOldFilename, string aNewFileName);
        private readonly ExecuteDelegate _onExecute;

        #region IAddinBeforeClientFileNameChangeHandler

        public void Execute(string aOldFileName, string aNewFileName)
        {
            if (_onExecute != null)
            {
                _onExecute(aOldFileName, aNewFileName);
            }
        }

        #endregion

        public BeforeChangeNameHandler(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
