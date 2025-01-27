using System.Threading.Tasks;

namespace BackgroundTask.Persistence.SQLite.Interfaces {
    public interface IInitializeDataBaseRepository {
        public Task InitializeDataBase();
    }
}
