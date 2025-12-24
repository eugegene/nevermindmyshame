using api.DTOs;
using api.Models;
using api.Repository.IReposotory;
using api.Services;
using api.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUnitOfWork unitOfWork, Mapper mapper) : ControllerBase
{
    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserById(string email)
    {
        var user = await unitOfWork.UserRepository.GetAsync(u => u.Email == email);
        if (user is null)
            return BadRequest(new { error = $"User {email} was not found" });

        var userDto = mapper.Map<UserDto>(user);
        return Ok(new { data = userDto });
    }

    //FIXME, ми навряд будь-коли будемо використовувати отримання всіх юзерів будь-де. Треба фільтри
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await unitOfWork.UserRepository.GetAllAsync();
        var userDtos = mapper.Map<List<UserDto>>(users);
        return Ok(new { data = userDtos });
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> RedactProfilePicture([FromBody] UserDto userDto)
    {
        if (userDto.ProfilePic is null)
            return BadRequest("File for ProfilePic cannot be null");

        var user = await unitOfWork.UserRepository.GetAsync(u => u.Email == userDto.Email);
        if (user is null)
            return BadRequest(new { error = $"User {userDto.Email} was not found" });

        var result = await UserService.SavePhoto(userDto.ProfilePic, user.Id,
            StaticDetails.GetUserProfileImagePath(user.Id));
        if (result.IsFailure)
            return BadRequest(new { error = result.Error });

        return Ok();
    }

    [HttpDelete("{email}")]
    [Authorize(Roles = "Admin")]
    public async Task<Result> DeleteUser(string email)
    {
        var user = await unitOfWork.UserRepository.GetAsync(u => u.Email == email);
        if (user is null)
            return Result.Fail($"User {email} was not found");

        await unitOfWork.UserRepository.Remove(user);
        return Result.Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
    {
        var isNotSameUser = User.Identity?.Name != userDto.Email;
        var isNotAdmin = !User.IsInRole("Admin");
        if (isNotSameUser && isNotAdmin)
            return BadRequest(new { error = "You are not authorized to update this user" });

        var user = await unitOfWork.UserRepository.GetAsync(u => u.Email == userDto.Email);
        if (user is null)
            return BadRequest(new { error = $"User {userDto.Email} was not found" });

        user = mapper.Map<User>(userDto);
        await unitOfWork.UserRepository.Update(user);
        return Ok();
    }
}