using Pixelgrid.DataModels;
using Pixelgrid.UI.Views;

namespace Pixelgrid.UI.Presenters
{
    public class BrezenheimIndexPresenter
    {
        private readonly BrezenheimIndexModel _brezenheimIndexModel;
        private readonly BrezenheimDataView _view;

        public BrezenheimIndexPresenter(BrezenheimIndexModel model, BrezenheimDataView view)
        {
            _brezenheimIndexModel = model;
            _view = view;
            _brezenheimIndexModel.IndexChangedEvent += OnIndexChangedEvent;
            _view.SetText("?");
        }

        private void OnIndexChangedEvent(int index)
        {
            _view.SetText(index > 0 ? "-" : "+");
        }
    }
}
