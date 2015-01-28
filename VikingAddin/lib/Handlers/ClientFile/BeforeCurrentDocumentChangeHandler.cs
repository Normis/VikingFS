using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.ClientFile
{
    public class BeforeCurrentDocumentChangeHandler : IAddinBeforeCurrentDocumentChangeHandler
    {
        public delegate void ExecuteDelegate(IAppTaxDocument OldDocument, IAppTaxDocument NewDocument);
        private readonly ExecuteDelegate _onExecute;

        #region IAddinBeforeCurrentDocumentChangeHandler

        public void Execute(IAppTaxDocument OldDocument, IAppTaxDocument NewDocument)
        {
            if (_onExecute != null)
                _onExecute(OldDocument, NewDocument);
        }

        #endregion

        public BeforeCurrentDocumentChangeHandler(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
