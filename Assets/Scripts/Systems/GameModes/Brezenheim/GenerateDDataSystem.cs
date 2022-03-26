using Leopotam.Ecs;
using Pixelgrid.DataModels;
using Pixelgrid.UI.Presenters;
using Pixelgrid.UI.Views;

namespace Pixelgrid.Systems.GameModes.Brezenheim 
{
    public sealed class GenerateDDataSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;

        private readonly BrezenheimDataView _brezenheimDataView = null;
        private readonly BrezenheimIndexModel _brezenheimIndexModel = null;

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                var presenter = new BrezenheimIndexPresenter(_brezenheimIndexModel, _brezenheimDataView);
            }
        }
    }
}