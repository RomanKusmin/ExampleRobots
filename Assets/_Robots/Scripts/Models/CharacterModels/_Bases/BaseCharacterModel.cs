using Configs.CharacterConfigs._Bases;
using EvaArchitecture._Services.FiniteStateMachineServices.Fsm._Models._Bases;
using EvaArchitecture.Models._Bases;

namespace Models.CharacterModels._Bases
{
    public abstract class BaseCharacterModel<TStateModel> : BaseThingModel<TStateModel>
        where TStateModel : BaseStateModel
    {
        public new BaseCharacterConfig Config => Config<BaseCharacterConfig>();
        
        public BaseCharacterModel()
        {
        }
        
        public BaseCharacterModel(BaseCharacterConfig config, TStateModel stateModel) : base(config, stateModel)
        {
        }
    }
}
