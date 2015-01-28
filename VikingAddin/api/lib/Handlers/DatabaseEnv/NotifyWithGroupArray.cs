using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.DatabaseEnv
{
    public class NotifyWithGroupArray : IAddinDatabaseEnvNotifyWithGroupArray
    {
        public delegate void ExecuteDelegate(IAppGroupArray aGroup);
        private readonly ExecuteDelegate _onExecute;

        #region IAddinDatabaseEnvNotifyWithGroupArray

        public void Execute(IAppGroupArray aGroup)
        {
            if (_onExecute != null)
                _onExecute(aGroup);
        }

        #endregion

        public NotifyWithGroupArray(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
