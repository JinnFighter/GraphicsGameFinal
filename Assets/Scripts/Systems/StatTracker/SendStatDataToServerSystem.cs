using Leopotam.Ecs;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Pixelgrid 
{
    public sealed class SendStatDataToServerSystem : IEcsRunSystem 
    {
        private EcsFilter<GameOverEvent> _filter;
        private EcsFilter<PlayerComponent> _playerFilter;
        private EcsFilter<StatData> _dataFilter;

        private GameModeConfiguration _gameModeConfiguration;

        void IEcsRunSystem.Run () 
        {
            if(!_filter.IsEmpty())
            {
                var statData = _dataFilter.Get1(0);
                var playerComponent = _playerFilter.Get1(0);
                var name = playerComponent.Name;
                var sendString = $"\"playerName\":\"{name}\",\"gameModeName\":\"{_gameModeConfiguration.Name}\",\"time\":{statData.TimeSpent},\"correctAnswers\":{statData.CorrectAnswers},\"wrongAnswers\":{statData.WrongAnswers}";

                try
                {
                    using(UnityWebRequest www = UnityWebRequest.Post("https://pixelstats.azurewebsites.net/api/statsapi", sendString))
                    {
                        www.SetRequestHeader("Content-Type", "application/json");
                        www.SendWebRequest();

                        if(www.result != UnityWebRequest.Result.Success)
                        {
                            Debug.LogError("ERROR: Request could not be completed");
                        }
                    }
                }
                catch(Exception)
                {
                    Debug.LogError("ERROR: Data could not be sent");
                }
            }
        }
    }
}