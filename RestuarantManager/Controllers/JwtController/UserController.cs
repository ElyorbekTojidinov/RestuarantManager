using Aplication.Interfaces.InterfacesJWT;
using Aplication.Interfaces.InterfacesProducts;
using Domain.JwtNotCreadDb;
using Domain.ModelsJWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace RestuarantManager.Controllers.JwtController;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class UserController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
    private readonly IConfiguration _configuration;
    //private readonly ILogger<UserController> _logger;
    public UserController(IJwtService jwtService,
        IUserRepository userRepository, IUserRefreshTokenRepository userRefreshTokenRepository,
        IConfiguration configuration)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
        _userRefreshTokenRepository = userRefreshTokenRepository;
        _configuration = configuration;
       // _logger = logger;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> RefreshToken(Token token)
    {
        //_logger.LogInformation($"{nameof(RefreshToken)}");
        var principal = _jwtService.GetPrincipalFromExpiredToken(token.AccessToken);
        var name = principal.FindFirstValue(ClaimTypes.Name);
        var user = await _userRepository.GetAsync(x => x.UserName == name);

        var credential = new UserCredentials
        {
            UserName = user.UserName,
            Password = user.Password
        };
        UserRefreshToken savedRefreshToken = await _userRefreshTokenRepository.GetSavedRefreshTokens(name, token.RefreshToken);
        if (savedRefreshToken == null && savedRefreshToken.RefreshToken != token.RefreshToken)
        {
            return Unauthorized("Invalid input");
        }
        //if (savedRefreshToken.Expiretime < DateTime.Now)
        //{
        //    return Unauthorized(" time limit of the token has expired !");
        //}

        var newJwt = await _jwtService.GenerateTokenAsync(user);
        if (newJwt == null)
        {
            return Unauthorized("Invalid input");
        }
        int min = 4;
        if (int.TryParse(_configuration["JWT:RefreshTokenExpiresTime"], out int _min))
        {
            min = _min;
        }
        UserRefreshToken refreshToken = new()
        {
            RefreshToken = newJwt.RefreshToken,
            UserName = name,
            Expiretime = DateTime.UtcNow.AddMinutes(min)
        };
        bool IsDeleted = await _userRefreshTokenRepository.DeleteUserRefreshTokens(name, token.RefreshToken);
        if (IsDeleted)
        {
            await _userRefreshTokenRepository.AddUserRefreshTokens(refreshToken);
        }
        else
        {
            return BadRequest();
        }
        return Ok(newJwt);

    }



    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromForm] UserCredentials userCredentials)
    {
        //_logger.LogInformation("Login is called");
        string hashedPsw = await _userRepository.ComputeHashAsync(userCredentials.Password);
        Users? user = await _userRepository.GetAsync(x => x.UserName == userCredentials.UserName && x.Password == hashedPsw); ;
        if (await _userRefreshTokenRepository.IsValidUserAsync(user))
        {
            return Unauthorized();
        }
        int min = 15;
        if (int.TryParse(_configuration["JWT:AccesTokenLifeTime"], out int _min))
        {
            min = _min;
        }
        var token = await _jwtService.GenerateTokenAsync(user);
        var refreshToken = new UserRefreshToken
        {
            UserName = userCredentials.UserName,
            Expiretime = DateTime.UtcNow.AddMinutes(min),
            RefreshToken = token.RefreshToken
        };
        await _userRefreshTokenRepository.UpdateUserRefreshToken(refreshToken);
        return Ok(token);

    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromBody] Users user)
    {
       // _logger.LogInformation($"Created {user.UserName}");
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _userRepository.CreateAsync(user);
            if (IsSuccess)
            {
                return Ok(user);
            }
        }
        return BadRequest();
    }

    [HttpGet]
    [Route("[action]{id}")]
    public async Task<IActionResult> GetById(int Id)
    {
       // _logger.LogInformation($"User Id: {Id}");
        Users? user = await _userRepository.GetAsync(x => x.UsersId == Id);
        if (user != null)
        {
            return Ok(user);
        }
        return BadRequest();
    }



    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
       // _logger.LogInformation($"{nameof(GetAll)}");
        var res = (await _userRepository.GetAllAsync()).Include(x => x.UserRoles).Select(x => new
        {
            x.UsersId,
            x.UserName,
            Role = x.UserRoles.Select(t => new
            {
                t.Role.Id,
                t.Role.Name
            })
        });
        return Ok(res);

    }


    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> Update([FromBody] Users user)
    {
       // _logger.LogInformation($" Updated : {user.UserName}");
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _userRepository.UpdateAsync(user);
            if (IsSuccess)
            {
                return Ok(user);
            }
        }
        return BadRequest();
    }



    [HttpDelete]
    [Route("[action]{id}")]
    public async Task<IActionResult> Delete(int id)
    {
       // _logger.LogInformation($"Deleted {id}");
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _userRepository.DeleteAsync(id);
            if (IsSuccess)
            {
                return Ok();
            }
        }
        return BadRequest();
    }
}

