namespace Core.Controllers._Bases.LocationControllers
{
    public abstract class BaseMultiPlayerLocationController<T> : BaseLocationController<T>
        where T : BaseMultiPlayerLocationController<T>
    {
    }
}
