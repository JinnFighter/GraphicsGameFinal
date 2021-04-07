using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class SelectMaxLineLengthSystem : IEcsInitSystem 
    {
        private RandomLinesGenerator _generator;
        private DifficultyConfiguration _difficultyConfiguration;
        
        public void Init() 
        {
            switch(_difficultyConfiguration.Difficulty)
            {
                case 1:
                    _generator.MaxLengthSum = 48;
                    break;
                case 2:
                    _generator.MaxLengthSum = 90;
                    break;
                default:
                    _generator.MaxLengthSum = 20;
                    break;
            }
        }
    }
}