using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.DatabaseEnv
{
    public class AfterAcceptUserInput : IAddinDatabaseEnvAfterAcceptUserInput
    {
        public delegate void ExecuteDelegate(IAppTaxCell aCell);
        private readonly ExecuteDelegate _onExecute;

        #region IAddinDatabaseEnvAfterAcceptUserInput

        public void Execute(IAppTaxCell aCell)
        {
            if (_onExecute != null)
                _onExecute(aCell);
        }

        #endregion

        public AfterAcceptUserInput(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
