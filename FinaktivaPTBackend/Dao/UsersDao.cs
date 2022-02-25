using FinaktivaPT.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FinaktivaPT.Dao
{
    public class UsersDao
    {
        private readonly string _connectionStrings;

        public UsersDao(IConfiguration configuration)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");

        }
        public async Task<List<GetUsuarios>> getUsers(int id)
        {
            SqlConnection sql = new SqlConnection(_connectionStrings);
            SqlCommand cmd = new SqlCommand("spGetUsers", sql);

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@id", id)
            };

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddRange(parameters.ToArray());


            var response = new List<GetUsuarios>();
            await sql.OpenAsync();

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    response.Add(
                    new GetUsuarios()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Nombre = (string)reader["nombre"],
                        Username = (string)reader["username"],
                        IdRol = Convert.ToInt32(reader["idRol"]),
                        Rol = (string)reader["rol"],
                        FechaCreacion = Convert.ToDateTime(reader["fechaCreacion"]),
                        FechaModificacion = Convert.ToDateTime(reader["fechaModificacion"])
                    });
                }

            }
            return response;
        }

        public async Task<respuesta> createUser(CCUsuarios user, int idUser)
        {
            SqlConnection sql = new SqlConnection(_connectionStrings);
            SqlCommand cmd = new SqlCommand("spCreateUser", sql);

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@nombre", user.Nombre),
                new SqlParameter("@username", user.Username),
                new SqlParameter("@password", user.Password),
                new SqlParameter("@rol", user.Rol),
                new SqlParameter("@idUsuario", idUser)
            };

            var mensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            var logRespuesta = new SqlParameter("@log", SqlDbType.Bit) { Direction = ParameterDirection.Output };

            parameters.Add(mensaje);
            parameters.Add(logRespuesta);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters.ToArray());

            await sql.OpenAsync();

            bool retorno = Convert.ToBoolean(cmd.ExecuteNonQuery());
            if (retorno)
            {
                return new respuesta()
                {
                    strMensaje = mensaje.Value.ToString(),
                    logRes = Convert.ToBoolean(logRespuesta.Value)
                };
            }
            else
            {
                return new respuesta()
                {
                    strMensaje = "No hay conexion con el servidor!",
                    logRes = false
                };
            }
        }

        public async Task<respuesta> updateUser(CUUsuarios user, int idUser)
        {
            SqlConnection sql = new SqlConnection(_connectionStrings);
            SqlCommand cmd = new SqlCommand("spUpdateUser", sql);

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@id", user.Id),
                new SqlParameter("@nombre", user.Nombre),
                new SqlParameter("@username", user.Username),
                new SqlParameter("@password", user.Password),
                new SqlParameter("@rol", user.Rol),
                new SqlParameter("@idUsuario", idUser)
            };

            var mensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            var logRespuesta = new SqlParameter("@log", SqlDbType.Bit) { Direction = ParameterDirection.Output };

            parameters.Add(mensaje);
            parameters.Add(logRespuesta);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters.ToArray());

            await sql.OpenAsync();

            bool retorno = Convert.ToBoolean(cmd.ExecuteNonQuery());
            if (retorno)
            {
                return new respuesta()
                {
                    strMensaje = mensaje.Value.ToString(),
                    logRes = Convert.ToBoolean(logRespuesta.Value)
                };
            }
            else
            {
                return new respuesta()
                {
                    strMensaje = "No hay conexion con el servidor!",
                    logRes = false
                };
            }
        }

        public async Task<respuesta> deleteUser(int id, int idUser)
        {
            SqlConnection sql = new SqlConnection(_connectionStrings);
            SqlCommand cmd = new SqlCommand("spDeleteUser", sql);

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@id", id),
                new SqlParameter("@idUsuario", idUser)
            };

            var mensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            var logRespuesta = new SqlParameter("@log", SqlDbType.Bit) { Direction = ParameterDirection.Output };

            parameters.Add(mensaje);
            parameters.Add(logRespuesta);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters.ToArray());

            await sql.OpenAsync();

            bool retorno = Convert.ToBoolean(cmd.ExecuteNonQuery());
            if (retorno)
            {
                return new respuesta()
                {
                    strMensaje = mensaje.Value.ToString(),
                    logRes = Convert.ToBoolean(logRespuesta.Value)
                };
            }
            else
            {
                return new respuesta()
                {
                    strMensaje = "No hay conexion con el servidor!",
                    logRes = false
                };
            }
        }


    }
}
