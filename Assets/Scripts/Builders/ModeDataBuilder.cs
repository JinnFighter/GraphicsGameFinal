using UnityEngine;

namespace Pixelgrid
{
    public class ModeDataBuilder : MonoBehaviour, IModeDataBuilder
    {
        private ModeData _modeData;

        // Start is called before the first frame update
        void Start()
        {
            ResetObject();
        }

        public void ResetObject() => _modeData = new ModeData { ModeName = "Brezenheim", Difficulty = 0 };

        public void SetDifficulty(int difficulty) => _modeData.Difficulty = difficulty;

        public void SetName(string modeName) => _modeData.ModeName = modeName;

        public ModeData GetResult() => _modeData;
    }
}
