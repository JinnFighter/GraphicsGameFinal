using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid.DataModels
{
    public class ColorPickerDataModel
    {
        public List<Color32> Colors = new List<Color32>();
        public int CurrentColor;
        public int ColorCount;
    }
}
