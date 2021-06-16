using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public class SetDifficultySystem : IEcsInitSystem 
    {
        private DifficultyConfiguration _difficultyConfiguration;

        public void Init() 
        {
            int difficulty;
            var prefsKey = "difficulty";
            if (PlayerPrefs.HasKey(prefsKey))
                difficulty = PlayerPrefs.GetInt(prefsKey);
            else
                difficulty = 0;

            _difficultyConfiguration.Difficulty = difficulty;
        }
    }
}