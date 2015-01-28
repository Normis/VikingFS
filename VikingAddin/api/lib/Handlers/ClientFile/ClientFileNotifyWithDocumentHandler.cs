using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.ClientFile
{
    public class ClientFileNotifyWithDocumentHandler : IAddinClientFileNotifyWithDocumentHandler
    {
        public delegate void ExecuteDelegate(IAppTaxDocument Document);
        private readonly ExecuteDelegate _onExecute;

        #region IAddinClientFileNotifyWithDocumentHandler

        public void Execute(IAppTaxDocument Document)
        {
            if (_onExecute != null)
                _onExecute(Document);
        }

        #endregion

        public ClientFileNotifyWithDocumentHandler(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
