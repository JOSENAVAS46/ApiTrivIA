using ApiTrivIA.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiTrivIA.Services
{
    public class AnimalService
    {
        private readonly IConfiguration _configuration;
        private readonly string cadenaSql;

        public AnimalService(IConfiguration configuration)
        {
            _configuration = configuration;
            this.cadenaSql = _configuration.GetConnectionString("CadenaSQL")!; ;
        }


        public async Task<List<Animal>> ListarAnimales()
        {
            string query = "sp_listarAnimales";
            using (var con = new SqlConnection(cadenaSql))
            {
                var lista = await con.QueryAsync<Animal>(query, commandType: CommandType.StoredProcedure);
                return lista.ToList();
            }
        }


    }
}
