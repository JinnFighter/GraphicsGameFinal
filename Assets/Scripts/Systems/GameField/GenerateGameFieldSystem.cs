using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Actions;
using Pixelgrid.Configurations.Script;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid.Systems.GameField 
{
    public sealed class GenerateGameFieldSystem : IEcsInitSystem 
    {
        private readonly EcsWorld _world = null;
        private readonly DifficultyConfiguration _difficultyConfiguration = null;
        private readonly GameFieldConfiguration _gameFieldConfiguration = null;
        private readonly GameFieldConfigs _gameFieldConfigs = null;

        public void Init() 
        {
            var difficulty = _difficultyConfiguration.Difficulty;

            int fieldSize = _gameFieldConfigs.Configs[difficulty].FieldSize;
            
            var grid = _gameFieldConfiguration.grid;

            var pixelPrefab = _gameFieldConfiguration.pixel;

            for (var i = 0; i < fieldSize; i++)
            {
                for(var j = 0; j < fieldSize; j++)
                {
                    var pixel = Object.Instantiate(pixelPrefab, grid.transform, true);
                    pixel.AddComponent<EcsUiClickAction>();

                    var entity = _world.NewEntity();
                    entity.Get<PixelComponent>();
                    ref var position = ref entity.Get<PixelPosition>();
                    position.position = new Vector2Int(i, j);
                    ref var pixelRef = ref entity.Get<PixelRef>();
                    pixelRef.pixel = pixel;
                    pixel.GetComponent<GridPixel>().entity = entity;
                    ref var imageRef = ref entity.Get<ImageRef>();
                    imageRef.Image = pixel.GetComponent<Image>();
                }
            }
        }
    }
}