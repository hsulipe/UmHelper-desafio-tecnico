using System;
using System.Text;
using Domain.Models.Dtos.Users;
using Infrastructure.Repositories.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.Exceptions.Base;
using Domain.Helpers;
using Domain.Models.Entities;
using Domain.Models.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using UmHelpFinanceiro.Services.IdentityTokens;
using UmHelpFinanceiro.Services.Users;

namespace UmHelpFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IIdentityTokenService _tokenService;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUserService _userService;

        public UsersController(IIdentityTokenService tokenService, IUserAccountRepository userAccountRepository, IUserService userService)
        {
            _tokenService = tokenService;
            _userAccountRepository = userAccountRepository;
            _userService = userService;
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult<UserAccountDto>> Register([FromBody] UserRegisterRequest model)
        {
            UserAccount user;
            try
            {
                user = (UserAccount) model;
            }
            catch (ArgumentException err)
            {
                return BadRequest(err.Message);
            }
            catch (DomainExceptionBase err)
            {
                return BadRequest(err.Message);
            }

            await _userService.RegisterAsync(user);

            return Ok((UserAccountDto) user);
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<UserAuthenticateResponseDto>> Authenticate([FromBody] UserAuthenticateRequestDto model)
        {
            var user = await _userAccountRepository.FindByAsync(x => x.Cpf.Number == new Cpf(model.Cpf).Number);

            if (user == null)
                return NotFound();
            else
            {
                if (!PasswordSaltedHelper.CompareHash(Encoding.ASCII.GetBytes(model.Password), user.Password.Hash, user.Password.Salt))
                {
                    return NotFound();
                }
            }
            
            var token = _tokenService.GenerateToken(user);
            return Ok(new UserAuthenticateResponseDto((UserAccountDto)user, token));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccountDto>> Get([FromRoute] Guid id)
        {
            var result = await _userAccountRepository.FindAsync(id);
            if (result == null)
                return NotFound();
            return Ok((UserAccountDto) result);
        }

        [HttpGet("{id}/balance")]
        public async Task<ActionResult<double>> GetBalance([FromRoute]Guid id)
        {
            var result = (await _userAccountRepository.FindAsync(id))?.CurrentBalance;
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
