namespace Services._Bases.Interfaces
{
    public interface IService
    {
        public abstract string ServiceName { get; }
        public abstract bool Init();
        public abstract bool DoDestroy();
    }
}
