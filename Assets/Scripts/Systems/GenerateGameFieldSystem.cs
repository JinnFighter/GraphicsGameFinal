using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid 
{
    public sealed class GenerateGameFieldSystem : IEcsInitSystem 
    {
        private EcsWorld _world;
        private DifficultyConfiguration _difficultyConfiguration;
        private GameFieldConfiguration _gameFieldConfiguration;

        public void Init() 
        {
            var difficulty = _difficultyConfiguration.Difficulty;

            int fieldSize;

            switch (difficulty)
            {
                case 1:
                    fieldSize = 15;
                    break;
                case 2:
                    fieldSize = 20;
                    break;
                default:
                    fieldSize = 10;
                    break;
            }

            _gameFieldConfiguration.fieldSize = fieldSize;
            var grid = _gameFieldConfiguration.grid;

            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = fieldSize;

            var pixelPrefab = _gameFieldConfiguration.pixel;

            var cellSize = pixelPrefab.GetComponent<Image>().sprite.rect.size;
            cellSize.x += 8;
            cellSize.y += 8;

            grid.cellSize = cellSize;

            for(var i = 0; i < fieldSize; i++)
            {
                for(var j = 0; j < fieldSize; j++)
                {
                    var pixel = Object.Instantiate(pixelPrefab);
                    pixel.transform.SetParent(grid.transform);

                    var entity = _world.NewEntity();
                    entity.Get<PixelComponent>();
                    ref var position = ref entity.Get<PixelPosition>();
                    position.position = new Vector2Int(i, j);
                    ref var pixelRef = ref entity.Get<PixelRef>();
                    pixelRef.pixel = pixel;
                }
            }
        }
    }
}