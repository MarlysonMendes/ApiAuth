using ApiAuth.Models;
using ApiAuth.Repositories;
using ApiAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] User user)
        {
            var userLogin = UserRepository.GetUser(user.Username, user.Password);

            if (userLogin == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(userLogin);
            //apaga a senha do usuário para não retornar no token
            userLogin.Password = "";
            return new
            {
                userLogin,
                token
            };
        }


    }
}