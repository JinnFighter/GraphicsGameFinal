using Leopotam.Ecs;
using Pixelgrid.Components.UI.ProgressBar;
using Pixelgrid.DataModels;
using UnityEngine;

namespace Pixelgrid.Systems.UI.ProgressBar
{
    public class UpdateProgressBarViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ProgressBarViewModel, ProgressBarView> _filter = null;
        private readonly AnswersModel _answersModel = null;
        
        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var viewModel = ref _filter.Get1(index);
                var view = _filter.Get2(index);

                if (viewModel.MaxValue != _answersModel.MaxAnswerCount)
                {
                    viewModel.MaxValue = _answersModel.MaxAnswerCount;
                    view.ProgressBar.MaxValue = viewModel.MaxValue;
                }

                if (viewModel.CurrentValue != _answersModel.CurrentAnswerCount)
                {
                    viewModel.CurrentValue = _answersModel.CurrentAnswerCount;
                    view.ProgressBar.CurrentValue = viewModel.CurrentValue;
                    var percentage = viewModel.CurrentValue / viewModel.MaxValue * 100;
                    Color color;
                    if (percentage < 61)
                        color = new Color32(220, 221, 225, 255);
                    else
                    if (percentage < 71)
                        color = new Color32(194, 54, 22, 255);
                    else
                    if (percentage < 91)
                        color = new Color32(251, 197, 49, 255);
                    else
                        color = new Color32(68, 189, 50, 255);

                    view.ProgressBar.Color = color;
                }
            }
        }
    }
}
