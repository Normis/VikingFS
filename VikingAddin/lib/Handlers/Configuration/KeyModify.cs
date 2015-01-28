using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.Configuration
{
    public class KeyModify : IAddinConfigurationKeyModifiedHandler
    {
        public delegate void ExecuteDelegate(string aLevel, string aSection, string aKey, string AValue);
        private readonly ExecuteDelegate _onExecute;

        #region

        public void Execute(string aLevel, string aSection, string aKey, string AValue)
        {
            if (_onExecute != null)
            {
                _onExecute(aLevel, aSection, aKey, AValue);
            }
        }

        #endregion

        public KeyModify(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }

    }
}
