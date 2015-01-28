using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.DatabaseEnv
{
    public class BeforeCalcHandler : IAddinBeforeCalcHandler
    {
        public delegate void ExecuteDelegate(IAppDocReturn aReturn);
        private readonly ExecuteDelegate _onExecute;

        #region IAddinNotifyHandler

        public void Execute(IAppDocReturn aReturn)
        {
            if (_onExecute != null)
                _onExecute(aReturn);
        }

        #endregion

        public BeforeCalcHandler(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
