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
        public async Task<PhoneModel?> AddPhone(PhoneModel model)
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
    }
}
