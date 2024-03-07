using System;
using Configs.CharacterConfigs.BotConfigs;
using Models.CharacterModels._Bases;

namespace Models.CharacterModels.BotsModels
{
    [Serializable]
    public class BotModel : BaseCharacterModel<BotStateModel>
    {
        public new BotConfig Config => Config<BotConfig>();
        
        public BotModel()
        {
        }
        
        public BotModel(BotConfig config, BotStateModel stateModel) : base(config, stateModel)
        {
        }
    }
}