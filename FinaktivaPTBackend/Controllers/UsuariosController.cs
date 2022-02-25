using FinaktivaPT.Dao;
using FinaktivaPT.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinaktivaPT.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly UsersDao dao;

        private readonly ILogger<UsuariosController> _logger;

        private readonly IHttpContextAccessor _accesor;

        private readonly int _idToken;

        public UsuariosController(UsersDao UserDao, ILogger<UsuariosController> logger, IHttpContextAccessor accessor)
        {
            dao = UserDao ?? throw new ArgumentNullException(nameof(UserDao));
            _logger = logger;
            _accesor = accessor;
            _idToken = Convert.ToInt32(_accesor?.HttpContext?.User.FindFirst(c => c.Type == "id").Value);
        }

        [HttpGet]
        public async Task<List<GetUsuarios>> getAll(int id)
        {
                return await dao.getUsers(id);            
        }

        [HttpPost]
        public async Task<respuesta> createUser(CCUsuarios user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Nombre))
                {
                    return new respuesta()
                    {
                        strMensaje = "Se necesita un Nombre para registrar!",
                        logRes = false
                    };
                }

                if (string.IsNullOrEmpty(user.Username))
                {
                    return new respuesta()
                    {
                        strMensaje = "Se necesita un Nombre de usuario para registrar!",
                        logRes = false
                    };
                }

                if (string.IsNullOrEmpty(user.Password))
                {
                    return new respuesta()
                    {
                        strMensaje = "Se necesita una contraseña de usuario para registrar!",
                        logRes = false
                    };
                }

                if (user.Rol == 0)
                {
                    return new respuesta()
                    {
                        strMensaje = "Se necesita un Rol para registrar!",
                        logRes = false
                    };
                }

                return await dao.createUser(user, _idToken);

            }catch(Exception ex)
            {
                return new respuesta()
                {
                    logRes = false,
                    strMensaje = ex.Message
                };
            }
            
        }

        [HttpPut]
        public async Task<respuesta> updateUser(CUUsuarios user)
        {
            try { 
            if (user.Id == 0)
            {
                return new respuesta()
                {
                    strMensaje = "Se necesita un id para actualizar",
                    logRes = false
                };
            }

            if (string.IsNullOrEmpty(user.Nombre))
            {
                return new respuesta()
                {
                    strMensaje = "Se necesita un Nombre para actualizar",
                    logRes = false
                };
            }

            if (string.IsNullOrEmpty(user.Username))
            {
                return new respuesta()
                {
                    strMensaje = "Se necesita un Nombre de usuario para actualizar",
                    logRes = false
                };
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                return new respuesta()
                {
                    strMensaje = "Se necesita una contraseña de usuario para actualizar!",
                    logRes = false
                };
            }

            if (user.Rol == 0)
            {
                return new respuesta()
                {
                    strMensaje = "Se necesita un Rol para actualizar",
                    logRes = false
                };
            }

            return await dao.updateUser(user, _idToken);

            }catch(Exception ex)
            {
                return new respuesta()
                {
                    logRes = false,
                    strMensaje = ex.Message
                };
            }
        }

        [HttpDelete]
        public async Task<respuesta> deleteUser(int id=0)
        {
            try
            {
                if(id == 0)
                {
                    return new respuesta()
                    {
                        strMensaje="Se necesita un id para eliminar",
                        logRes=false
                    };
                }

                return await dao.deleteUser(id, _idToken);
            }
            catch (Exception ex)
            {
                return new respuesta()
                {
                    logRes = false,
                    strMensaje = ex.Message
                };
            }
            
        }

    }
}
