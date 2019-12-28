using System.Linq;

namespace Project.Core.Connection
{
    public class TestDataBase
    {
        public TestDataBase()
        {
            ConnectionDb connectionDb = new ConnectionDb();
            connectionDb.TableUser.ToList();
        }
    }
}
