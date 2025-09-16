using ApiTrivIA.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiTrivIA.Services
{
    public class LoginService
    {
        private readonly IConfiguration _configuration;
        private readonly string cadenaSql;

        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
            cadenaSql = _configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<Usuario> ValidarUsuario(Login login)
        {
            string query = "sp_validarUsuario";
            var parametros = new DynamicParameters();
            parametros.Add("@usuario", login.Usuario, dbType: DbType.String);
            parametros.Add("@contrasenia", login.Contrasenia, dbType: DbType.String);

            using (var con = new SqlConnection(cadenaSql))
            {
                // Ejecutamos el procedimiento almacenado
                var usuario = await con.QueryFirstOrDefaultAsync<Usuario>(query, parametros, commandType: CommandType.StoredProcedure);

                // Si el usuario no existe o las credenciales son incorrectas, retornamos null
                return usuario;
            }
        }
    }
}
