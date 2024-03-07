using System;
using Configs.CharacterConfigs.PlayerConfigs;
using Models.CharacterModels._Bases;

namespace Models.CharacterModels.PlayerModels
{
    [Serializable]
    public class PlayerModel : BaseCharacterModel<PlayerStateModel>
    {
        public new PlayerConfig Config => Config<PlayerConfig>();
        
        public PlayerModel()
        {
        }
        
        public PlayerModel(PlayerConfig config, PlayerStateModel stateModel) : base(config, stateModel)
        {
        }
    }
}
