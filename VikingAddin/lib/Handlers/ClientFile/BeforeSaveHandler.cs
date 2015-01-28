using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.ClientFile
{
    public class BeforeSaveHandler : IAddinBeforeClientFileSaveHandler
    {
        public delegate void ExecuteDelegate(string aFileName, out bool aAccept);
        private readonly ExecuteDelegate _onExecute;

        #region IAddinBeforeClientFileSaveHandler

        public void Execute(string aFileName, out bool aAccept)
        {
            if (_onExecute != null)
                _onExecute(aFileName, out aAccept);
            else
                aAccept = true;
        }

        #endregion

        public BeforeSaveHandler(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
