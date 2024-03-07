using Core.Controllers._Bases.LocationControllers;

namespace Core.Controllers.LocationControllers
{
    public class CoreSinglePlayerLocationController<T> : BaseSinglePlayerLocationController<T>
        where T : CoreSinglePlayerLocationController<T>
    {
    }
}
