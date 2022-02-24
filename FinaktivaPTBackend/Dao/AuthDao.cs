using FinaktivaPT.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FinaktivaPT.Dao
{
    
    public class AuthDao
    {
        private readonly string _connectionStrings;

        public AuthDao(IConfiguration configuration)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task<respuesta> Login(string username, string password)
        {
            SqlConnection sql = new SqlConnection(_connectionStrings);
            SqlCommand cmd = new SqlCommand("spAuthLogin", sql);

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };

            var mensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            var logRespuesta = new SqlParameter("@log", SqlDbType.Bit) { Direction = ParameterDirection.Output };

            parameters.Add(mensaje);
            parameters.Add(logRespuesta);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddRange(parameters.ToArray());


            var response = new List<CUUsuarios>();
            await sql.OpenAsync();

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    response.Add(
                    new CUUsuarios()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Nombre = (string)reader["nombre"],
                        Username = (string)reader["username"],
                        Rol = Convert.ToInt32(reader["idRol"]),
                    });
                }

            }
            return new respuesta()
            {
                result = response.Count > 0 ? response[0] : null,
                strMensaje = mensaje.Value.ToString(),
                logRes = Convert.ToBoolean(logRespuesta.Value)
            };

        }

    }
}
