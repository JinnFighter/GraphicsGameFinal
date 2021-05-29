using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class LoadPlayerSystem : IEcsInitSystem 
    {
        private readonly EcsWorld _world;
        
        public void Init()
        {
            var key = "PlayerName";
            var playerName = PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString("PlayerName") : "test";

            var entity = _world.NewEntity();
            ref var playerComponent = ref entity.Get<PlayerComponent>();
            playerComponent.Name = playerName;
        }
    }
}