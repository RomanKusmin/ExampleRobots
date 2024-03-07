using Core.Controllers.LocationControllers;
using EvaArchitecture.EvaHelpers;
using EvaArchitecture.Logger;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Controllers.LocationControllers
{
    public class SinglePlayerLocationController : CoreSinglePlayerLocationController<SinglePlayerLocationController>
    {
        [SerializeField] private GameObject _botsCreationArea;
        [SerializeField] private float _cellBorder;

        protected override bool CreateBots(Transform parent)
        {
            var locationConfig = _locationConfig;
            if (locationConfig.IsNull())
            {
                Log.Error(() => $"SinglePlayerLocationController, CreateBots FAILED, locationConfig.IsNull");
                return false;
            }
            
            var botsCount = _locationConfig.BotsCount;
            if (botsCount <= 0)
                return true;

            var botConfig = locationConfig.BotConfig;
            if (botConfig.IsNull())
                return false;

            var botPrefab= botConfig.Prefab;
            if (botPrefab.IsNull())
                return false;

            var botsCreationStep = _botsCreationArea.transform.localScale.z / botsCount;
            for (var i = 0; i < botsCount; i++)
            {
                var tr = _botsCreationArea.transform;
                var pos = tr.position;
                
                var z = (pos.z - tr.localScale.z / 2) 
                        + (i * botsCreationStep) 
                        + Random.Range(-(botsCreationStep - _cellBorder) / 2, (botsCreationStep - _cellBorder) / 2);

                var bot = Instantiate(botPrefab, _botsParent.transform);
                bot.transform.position = new Vector3(pos.x, pos.y, z);
                bot.name = $"{botPrefab.name} ({i})";
            }
            return true;
        }
    }
}
