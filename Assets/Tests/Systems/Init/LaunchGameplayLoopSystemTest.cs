using System.Collections;
using Leopotam.Ecs;
using NUnit.Framework;
using Pixelgrid;
using UnityEngine.TestTools;

public class LaunchGameplayLoopSystemTest
{
    private EcsWorld _world;
    private EcsSystems _systems;

    private LaunchGameplayLoopSystem _system;

    [SetUp]
    public void Setup()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _system = new LaunchGameplayLoopSystem();
        _systems.Add(_system);
    }
    
    [UnityTest]
    public IEnumerator LaunchGameplayLoopSystemTestWithEnumeratorPasses()
    {
        var filter = _world.GetFilter(typeof(EcsFilter<RestartGameEvent>));
        
        _systems.Init();
        
        Assert.False(filter.IsEmpty());
        yield return null;
    }
}
