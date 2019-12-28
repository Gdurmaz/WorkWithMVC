using Project.Core.Connection;

namespace Project.Core.Singleton
{
    public class SingletonBase
    {
        protected static ConnectionDb _connDb;
        private static object _lock = new object();
        protected SingletonBase()
        {
            GetConnection();
        }
        private ConnectionDb GetConnection()
        {
            if (_connDb==null)
            {
                lock (_lock)
                {
                    if (_connDb==null)
                    {
                        _connDb = new ConnectionDb();
                    }
                }
            }
            return _connDb;
        }
    }
}
