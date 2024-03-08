using Configs.LocationConfigs._Bases;
using Models.CharacterModels.PlayerModels;

namespace Core.Controllers._Bases.Interfaces
{
    public interface IBaseLocationController
    {
        public BaseLocationConfig LocationConfig { get; }
        public PlayerModel PlayerModel { get; }
        public bool Init(BaseLocationConfig locationConfig);
        public bool DestroyLocation();
    }
}
