using System;
using Configs.CharacterConfigs.BotConfigs;
using Models.CharacterModels._Bases;

namespace Models.CharacterModels.BotsModels
{
    [Serializable]
    public class BotStateModel : BaseCharacterStateModel
    {
        public new BotStateConfig Config => Config<BotStateConfig>();
        
        public BotStateModel()
        {
        }
        
        public BotStateModel(BotStateConfig config) : base(config)
        {
        }
    }
}
