using Microsoft.AspNetCore.Mvc;
using MediatR;
using GigaHack2025.UseCases.Commands.Users;
using GigaHack2025.Shared.DTOs;
using FluentValidation;
using GigaHack2025.Core.Enums;

namespace GigaHack2025.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<RegisterUserCommand> _validator;

    public AuthController(IMediator mediator, IValidator<RegisterUserCommand> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register([FromBody] RegisterUserDto request)
    {
        try
        {
            var command = new RegisterUserCommand(
                request.Name,
                request.Surname,
                request.Email,
                request.PhoneNumber,
                request.CompanyCode,
                request.Password,
                request.Role
            );

            // Validate the command
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = validationResult.Errors.Select(e => new {
                        field = e.PropertyName,
                        message = e.ErrorMessage
                    })
                });
            }

            // Execute the command
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpPost("register/admin")]
    public async Task<ActionResult<UserResponseDto>> RegisterAdmin([FromBody] RegisterUserDto request)
    {
        try
        {
            // Force role to Admin for this endpoint
            var command = new RegisterUserCommand(
                request.Name,
                request.Surname,
                request.Email,
                request.PhoneNumber,
                request.CompanyCode,
                request.Password,
                UserRole.Admin
            );

            // Validate the command
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = validationResult.Errors.Select(e => new {
                        field = e.PropertyName,
                        message = e.ErrorMessage
                    })
                });
            }

            // Execute the command
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }
}
