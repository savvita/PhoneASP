using PhoneDB.Model;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace PhoneDB.Repositories
{
    public class SQLPhoneRepository : IPhoneRepository
    {
        private DBConfig configuration;

        public SQLPhoneRepository(DBConfig configuration)
        {
            this.configuration = configuration;
        }
        public async Task<PhoneModel> AddPhoneAsync(PhoneModel model)
        {
            using IDbConnection connection = new SqlConnection(configuration.ConnectionString);
            var id = (await connection.QueryFirstAsync<int>("insert into Phones values (@Producer, @Model, @Price); select SCOPE_IDENTITY()",
                model));
            model.Id = id;

            return model;
        }

        public async Task<IEnumerable<PhoneModel>> GetAllPhonesAsync()
        {
            using IDbConnection connection = new SqlConnection(configuration.ConnectionString);
            return await connection.QueryAsync<PhoneModel>("select * from Phones");
        }

        public async Task<PhoneModel?> GetPhoneByIdAsync(int id)
        {
            using IDbConnection connection = new SqlConnection(configuration.ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<PhoneModel>("select * from Phones where Id = @Id", new { Id = id });
        }

        public async Task<bool> RemoveAsync(int id)
        {
            using IDbConnection connection = new SqlConnection(configuration.ConnectionString);

            var rows = await connection.ExecuteAsync("delete Phones where Id = @Id", new { Id = id });

            return rows != 0;
        }

        public async Task<PhoneModel?> UpdatePhoneAsync(PhoneModel model)
        {
            using IDbConnection connection = new SqlConnection(configuration.ConnectionString);

            var rows = (await connection.ExecuteAsync("update Phones set [Producer] = @Producer, [Model] = @Model, [Price] = @Price where Id = @Id",
                model));

            return rows == 0 ? null : model;
        }
    }
}
