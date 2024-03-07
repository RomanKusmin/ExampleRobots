namespace Core.Patterns
{
    public class BaseSingleton<T>
        where T: class, new()
    {
        private static T _instance;

        public static T Instance => _instance;

        public BaseSingleton()
        {
            _instance = this as T;
        }
    }
}
