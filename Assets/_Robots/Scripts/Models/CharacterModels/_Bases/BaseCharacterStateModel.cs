using System;
using Configs.CharacterConfigs._Bases;
using EvaArchitecture._Services.FiniteStateMachineServices.Fsm._Models._Bases;

namespace Models.CharacterModels._Bases
{
    [Serializable]
    public abstract class BaseCharacterStateModel : BaseThingStateModel
    {
        public new BaseCharacterStateConfig Config => Config<BaseCharacterStateConfig>();
        
        public BaseCharacterStateModel()
        {
        }
        
        public BaseCharacterStateModel(BaseCharacterStateConfig config) : base(config)
        {
        }
    }
}
