using System.Threading.Tasks;
using Systeme.DAL.Context;

namespace AppWpf.Data
{
    internal class DbInitializer
    {
        private readonly ApplicationDbContext _db;
        public DbInitializer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task InitializeAsync()
        {
            //Если он не существует БД, никаких действий не выполняется. Если она существует, база данных удаляется.
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            
            _db.Database.EnsureCreated();
        }
    }
}
