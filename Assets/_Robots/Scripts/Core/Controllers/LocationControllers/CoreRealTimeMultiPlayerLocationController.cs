using Core.Controllers._Bases.LocationControllers;

namespace Core.Controllers.LocationControllers
{
    public class CoreRealTimeMultiPlayerLocationController<T> : BaseMultiPlayerLocationController<T>
        where T: CoreRealTimeMultiPlayerLocationController<T>
    {
    }
}
