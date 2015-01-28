using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.ClientFile
{
    public class BeforeReturnStatusChangeHandler : IAddinReturnStatusChange
    {
        public delegate void ExecuteDelegate(IAppDocReturn aDocReturn, string AStatusName, int aOldValue, int AValue);
        private readonly ExecuteDelegate _onExecute;

        #region IAddinBeforeClientFileSaveHandler

        public void Execute(IAppDocReturn aDocReturn, string AStatusName, int aOldValue, int AValue)
        {
            if (_onExecute != null)
                _onExecute(aDocReturn, AStatusName, aOldValue, AValue);
        }

        #endregion

        public BeforeReturnStatusChangeHandler(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
