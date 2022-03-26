using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using Pixelgrid.DataModels;
using UnityEngine;

namespace Pixelgrid.Systems.GameModes.ColorPicker {

    public sealed class CreateColorContainerSystem : IEcsInitSystem
    {
        private readonly ColorContainerModel _colorContainerModel = null;
        
        public void Init()
        {
            var colors = new List<Color32>();
            for (var i = 0; i < 6; i++)
            {
                colors.Add(new Color32
                {
                    r = GetColorComponentValue(),
                    g = GetColorComponentValue(),
                    b = GetColorComponentValue(),
                    a = 255
                });
            }

            _colorContainerModel.Colors = colors;
        }

        private byte GetColorComponentValue() => Convert.ToByte(UnityEngine.Random.Range(0, 255));
    }
}