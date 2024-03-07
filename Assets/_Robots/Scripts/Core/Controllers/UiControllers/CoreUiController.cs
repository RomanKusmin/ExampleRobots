using EvaArchitecture.Controllers._Bases;

namespace Core.Controllers.UiControllers
{
    public class CoreUiController<T> : SingletonMonoBehaviour<T>
        where T : CoreUiController<T>
    {
    }
}
