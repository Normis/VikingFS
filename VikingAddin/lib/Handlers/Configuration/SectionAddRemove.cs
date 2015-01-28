using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.Configuration
{
    public class SectionAddRemove : IAddinConfigurationAddRemoveSectionHandler 
    {
        public delegate void ExecuteDelegate(string aSectionName);
        private readonly ExecuteDelegate _onExecute;

        #region

        public void Execute(string aSectionName)
        {
            if (_onExecute != null)
            {
                _onExecute(aSectionName);
            }
        }

        #endregion

        public SectionAddRemove(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
