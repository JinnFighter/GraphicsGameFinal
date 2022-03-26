using Leopotam.Ecs;
using Pixelgrid.DataModels;
using Pixelgrid.UI.Presenters;
using UnityEngine.UI;

namespace Pixelgrid 
{
    public sealed class GenerateDDataSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        private readonly EcsFilter<Brezenheim_D_Data> _filter = null;

        private readonly BrezenheimDataContainer _brezenheimDataContainer = null;
        private readonly BrezenheimIndexModel _brezenheimIndexModel = null;

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                var presenter = new BrezenheimIndexPresenter(_brezenheimIndexModel, _brezenheimDataContainer.View);
            }
        }
    }
}