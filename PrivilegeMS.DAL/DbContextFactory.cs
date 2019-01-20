using System.Runtime.Remoting.Messaging;
using System.Data.Entity;

namespace PrivilegeMS.DAL
{
    public class DbContextFactory
    {
        public static DbContext createDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("dbContext");
            if (dbContext == null)
            {
                dbContext = new PrivilegeMS.Model.PrivilegeEntities();
                CallContext.SetData("dbContext", dbContext);
            }
            return dbContext;
        }
    }
}
