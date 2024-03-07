namespace Core.Controllers._Bases.LocationControllers
{
    public abstract class BaseSinglePlayerLocationController<T> : BaseLocationController<T>
        where T: BaseSinglePlayerLocationController<T>
    {
    }
}
