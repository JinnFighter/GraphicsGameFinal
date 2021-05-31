using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    public class GridPixel : MonoBehaviour
    {
        private Image _frontImage;
        public EcsEntity entity;

        public void Start()
        {
            _frontImage = GetComponent<Image>();
        }

        public void UpdateSprite(Sprite sprite) => _frontImage.sprite = sprite;
    }
}
