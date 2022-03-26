using Configurations.Script;
using Leopotam.Ecs;

namespace Pixelgrid.Systems.GameModes.Brezenheim 
{
    public sealed class SelectMaxLineLengthSystem : IEcsInitSystem 
    {
        private readonly RandomLinesGenerator _generator = null;
        private readonly DifficultyConfiguration _difficultyConfiguration = null;
        private readonly BrezenheimConfigs _brezenheimConfigs = null;
        
        public void Init()
        {
            _generator.MaxLengthSum = _brezenheimConfigs[_difficultyConfiguration.Difficulty].MaxLengthSum;
        }
    }
}