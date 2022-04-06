using Leopotam.Ecs;

namespace Pixelgrid.Components.UI.ProgressBar
{
    public struct ProgressBarViewModel : IEcsAutoReset<ProgressBarViewModel>
    {
        public float CurrentValue;
        public float MaxValue;
        public void AutoReset(ref ProgressBarViewModel c)
        {
            c.CurrentValue = -1;
            c.MaxValue = -1;
        }
    }
}
