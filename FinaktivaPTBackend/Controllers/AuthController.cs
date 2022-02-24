using FinaktivaPT.Dao;
using FinaktivaPT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FinaktivaPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthDao dao;

        private readonly ITokenService _authService;
        public AuthController(AuthDao authDao, ITokenService authService)
        {
            dao = authDao ?? throw new ArgumentNullException(nameof(authDao));
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<respuesta> login([FromBody] AuthUser login)
        {
            try
            {
                var res = await dao.Login(login.Username, login.Password);

                string token;
                if (!res.logRes)
                {
                    return new respuesta()
                    {
                        logRes = false,
                        strMensaje = res.strMensaje
                    };
                }
                if (_authService.IsAuthenticated(res.result, out token))
                {
                    return new respuesta()
                    {
                        logRes = true,
                        strMensaje = token
                    };
                }
                else
                {
                    return new respuesta()
                    {
                        logRes = false,
                        strMensaje = "Error de servidor"
                    };
                }

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
