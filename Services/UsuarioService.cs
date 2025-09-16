using ApiTrivIA.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiTrivIA.Services
{
    public class UsuarioService
    {
        private readonly IConfiguration _configuration;
        private readonly string cadenaSql;

        public UsuarioService(IConfiguration configuration)
        {
            _configuration = configuration;
            cadenaSql = _configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Usuario>> ListarUsuario()
        {
            string query = "sp_listarUsuario";
            using(var con = new SqlConnection(cadenaSql))
            {
                var lista = await con.QueryAsync<Usuario>(query,commandType:CommandType.StoredProcedure);
                return lista.ToList();
            }
        }


        public async Task<Usuario> ObtenerUsuario(string usuario)
        {
            string query = "sp_obtenerUsuario";
            var parametros = new DynamicParameters();
            parametros.Add("@usuario", usuario, dbType:DbType.String);
            using (var con = new SqlConnection(cadenaSql))
            {
                var usuarioObtenido = await con.QueryFirstOrDefaultAsync<Usuario>(query,parametros,commandType: CommandType.StoredProcedure);
                return usuarioObtenido;
            }
        }


        public async Task<string> CrearUsuario(Usuario objeto)
        {
            string query = "sp_crearUsuario";
            var parametros = new DynamicParameters();
            parametros.Add("@usuario", objeto.usuario, dbType: DbType.String);
            parametros.Add("@correo", objeto.correo, dbType: DbType.String);
            parametros.Add("@contrasenia", objeto.contrasenia, dbType: DbType.String);
            parametros.Add("@msgError", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            using (var con = new SqlConnection(cadenaSql))
            {
                await con.ExecuteAsync(query, parametros, commandType: CommandType.StoredProcedure);
                bool msgError = parametros.Get<bool>("@msgError");
                return msgError ? "TRUE" : "FALSE";
            }
        }



        public async Task<string> EditarUsuario(Usuario objeto)
        {
            string query = "sp_editarUsuario";
            var parametros = new DynamicParameters();
            parametros.Add("@uidUsuario", objeto.uidUsuario, dbType: DbType.String);
            parametros.Add("@usuario", objeto.usuario, dbType: DbType.String);
            parametros.Add("@correo", objeto.correo, dbType: DbType.String);
            parametros.Add("@contrasenia", objeto.contrasenia, dbType: DbType.String);
            parametros.Add("@msgError", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
            using (var con = new SqlConnection(cadenaSql))
            {
                await con.ExecuteAsync(query, parametros, commandType: CommandType.StoredProcedure);
                return parametros.Get<string>("@msgError");
            }
        }

        public async Task EliminarUsuario(string uidUsuario)
        {
            string query = "sp_eliminarUsuario";
            var parametros = new DynamicParameters();
            parametros.Add("@uidUsuario", uidUsuario, dbType: DbType.String);
            parametros.Add("@msgError", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
            using (var con = new SqlConnection(cadenaSql))
            {
                await con.ExecuteAsync(query, parametros, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
