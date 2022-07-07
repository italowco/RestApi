using Microsoft.EntityFrameworkCore;

namespace RestApi.Infraestructure.Data
{
    public interface IDataContext
    {
        DbSet<T> Set<T>() where T : class;

        public int SaveChanges();
    }
}
