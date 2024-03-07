using Core.Controllers._Bases.LocationControllers;

namespace Core.Controllers.LocationControllers
{
    public class CorePvEMultiPlayerLocationController<T> : BaseMultiPlayerLocationController<T>
        where T: CorePvEMultiPlayerLocationController<T>
    {
    }
}
