using BackgroundTask.Persistence.SQLite.Interfaces;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

namespace BackgroundTask.Persistence.SQLite.Persistence.Repositories {
    public class InitializeDataBaseRepository : IInitializeDataBaseRepository {
        private string _connectionString { get; set; }

        public InitializeDataBaseRepository( string connectionString ) {
            this._connectionString = connectionString;
            InitializeDataBase();
        }



        public Task InitializeDataBase() {
            this.CreateStructure();
            return Task.CompletedTask;
        }


        public void CreateStructure() {
            using( var connection = new SqliteConnection( _connectionString ) ) {
                connection.Open();

                var createTablesCommand = @"CREATE TABLE IF NOT EXISTS Commands (
                                                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                                CommandName TEXT NOT NULL,
                                                Payload TEXT NOT NULL,
                                                Status TEXT NOT NULL,
                                                ExecutionStartTime DATETIME NOT NULL,
                                                ExecutionEndTime DATETIME,
                                                ErrorMessage TEXT);";

                using( var command = connection.CreateCommand() ) {
                    command.CommandText = createTablesCommand;
                    command.ExecuteNonQuery();
                };
            };
        }
    }
}
