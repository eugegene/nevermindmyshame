using api.DTOs;
using api.Identity;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using static api.DTOs.RequestTypes;

namespace api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    /// <summary>
    ///     Register new user and get UserDto
    /// </summary>
    /// <param name="request">UserDto object. Just use make Email and Password in request object</param>
    /// <returns>JWT token string</returns>
    /// <response code="200">Successfully registered</response>
    /// <response code="400">Wrong login or password</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("register")]
    public ObjectResult RegisterUser([FromBody] UserDto request)
    {
        var result = AuthService.RegisterUser(request, request.Role);
        if (result.IsSuccess)
            return Ok(new { data = result.Value });
        return BadRequest(new { error = result.Error });
    }

    /// <summary>
    ///     Log in and get UserDto
    /// </summary>
    /// <param name="request">Standard .Net LoginRequest object. Just use make Email and Password in request object</param>
    /// <returns>JWT token string</returns>
    /// <response code="200">Successfully logged in</response>
    /// <response code="400">Wrong login or password</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("login")]
    public ObjectResult LoginUser([FromBody] LoginRequest request)
    {
        var result = AuthService.LoginUser(request.Email, request.Password);
        if (result.IsSuccess)
            return Ok(new { data = result.Value });
        return BadRequest(new { error = result.Error });
    }

    /// <summary>
    ///     Send access token and get UserDto with renewed access and refresh tokens
    /// </summary>
    /// <param></param>
    /// <param name="request"></param>
    /// <returns>UserDto</returns>
    /// <response code="200">Successfully authorized</response>
    /// <response code="400">JWT expired / not correct</response>
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [HttpPost("renewToken")]
    public ObjectResult RenewToken([FromBody] RenewTokenRequest request)
    {
        var result = AuthService.RenewToken(request.RefreshToken);
        if (result.IsSuccess)
            return Ok(new { Data = result.Value });

        return BadRequest(new { error = result.Error} );
    }

    [Authorize(Policy = IdentityData.PolicyAdmin)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [HttpGet("test")]
    public IActionResult TestSuperAdmin()
    {
        return Ok("oh, hi... You've made it! Here it felt lonely without you");
    }
}