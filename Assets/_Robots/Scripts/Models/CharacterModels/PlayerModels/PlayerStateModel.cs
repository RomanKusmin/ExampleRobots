using System;
using Configs.CharacterConfigs.PlayerConfigs;
using Models.CharacterModels._Bases;

namespace Models.CharacterModels.PlayerModels
{
    [Serializable]
    public class PlayerStateModel : BaseCharacterStateModel
    {
        public new PlayerStateConfig Config => Config<PlayerStateConfig>();
        
        public PlayerStateModel()
        {
        }
        
        public PlayerStateModel(PlayerStateConfig config) : base(config)
        {
        }
    }
}
