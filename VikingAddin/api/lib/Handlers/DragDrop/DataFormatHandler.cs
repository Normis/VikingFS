using TaxprepAddinAPI;

namespace TxpAddinLibrary.Handlers.DragDrop
{
    public class DataFormatHandler : IAddinDragDropDataFormatHandler
    {
        public delegate dynamic ExecuteDelegate(IAppCellSelectionIter aSelection);
        private readonly ExecuteDelegate _onExecute;

        #region IAddinDragDropDataFormatHandler

        public dynamic GetData(IAppCellSelectionIter aSelection)
        {
            if (_onExecute != null)
                return _onExecute(aSelection);
            return null;
        }

        #endregion

        public DataFormatHandler(ExecuteDelegate onExecute)
        {
            _onExecute = onExecute;
        }
    }
}
