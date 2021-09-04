using System.Collections;
using Leopotam.Ecs;
using NUnit.Framework;
using Pixelgrid;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class UpdateImageSpritesSystemTest
{
    private EcsWorld _world;
    private EcsSystems _systems;
    private UpdateImageSpritesSystem _system;
    private Image _image;

    [SetUp]
    public void Setup()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _system = new UpdateImageSpritesSystem();
        _systems.Add(_system).Init();

        var gameObject = new GameObject("test");
        _image = gameObject.AddComponent<Image>();
        var entity = _world.NewEntity();
        ref var imageRef = ref entity.Get<ImageRef>();
        imageRef.Image = _image;
        ref var spriteEvent = ref entity.Get<UpdateSpriteImageEvent>();
        spriteEvent.Sprite = Sprite.Create(new Texture2D(10, 10), new Rect(0, 0, 10, 10), Vector2.zero);
    }
    
    [UnityTest]
    public IEnumerator UpdateImageSpritesSystemTestWithEnumeratorPasses()
    {
        var filter = _world.GetFilter(typeof(EcsFilter<ImageRef, UpdateSpriteImageEvent>));
        
        _systems.Run();

        foreach (var index in filter)
        {
            var entity = filter.GetEntity(index);
            var imageRef = entity.Get<ImageRef>();
            var spriteEvent = entity.Get<UpdateSpriteImageEvent>();
            Assert.AreSame(spriteEvent.Sprite, imageRef.Image.sprite);
        }
        yield return null;
    }
}
