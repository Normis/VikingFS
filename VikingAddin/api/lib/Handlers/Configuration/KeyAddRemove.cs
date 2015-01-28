using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.Configuration
{
    public class KeyAddRemove : IAddinConfigurationAddRemoveKeyHandler
    {
        public delegate void ExecuteDelegate(string aSection, string aKey);
        private readonly ExecuteDelegate _onExecute;

        #region

        public void Execute(string aSection, string aKey)
        {
            if (_onExecute != null)
            {
                _onExecute(aSection, aKey);
            }
        }

        #endregion

        public KeyAddRemove(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }

    }
}
