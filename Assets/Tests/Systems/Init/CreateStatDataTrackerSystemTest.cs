using System.Collections;
using Leopotam.Ecs;
using NUnit.Framework;
using Pixelgrid;
using UnityEngine.TestTools;

public class CreateStatDataTrackerSystemTest
{
    private EcsWorld _world;
    private EcsSystems _systems;

    private CreateStatDataTrackerSystem _system;

    [SetUp]
    public void Setup()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _system = new CreateStatDataTrackerSystem();
        _systems.Add(_system);
    }
    
    [UnityTest]
    public IEnumerator Init()
    {
        var filter = _world.GetFilter(typeof(EcsFilter<StatData, Stopwatch, StatTrackerStopwatch>));
        
        _systems.Init();
        
        Assert.False(filter.IsEmpty());
        yield return null;
    }
}
