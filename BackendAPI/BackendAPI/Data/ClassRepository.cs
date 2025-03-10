using BackendAPI.Models;

namespace BackendAPI.Data
{
    public class ClassRepository : BaseRepository<Class>
    {
        public ClassRepository(AppDbContext context) : base(context) { }
    }
}
