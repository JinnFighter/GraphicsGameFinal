using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    public class GridPixel : MonoBehaviour
    {
        [SerializeField] private Image _frontImage;
        public EcsEntity entity;

        public void UpdateSprite(Sprite sprite) => _frontImage.sprite = sprite;
    }
}
