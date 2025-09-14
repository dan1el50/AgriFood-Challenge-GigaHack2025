using Microsoft.AspNetCore.Mvc;
using MediatR;
using GigaHack2025.UseCases.Commands.FarmerProfiles;
using GigaHack2025.UseCases.Queries.FarmerProfiles;
using GigaHack2025.Shared.DTOs;
using FluentValidation;

namespace GigaHack2025.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FarmerProfileController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateFarmerProfileCommand> _createValidator;

    public FarmerProfileController(
        IMediator mediator,
        IValidator<CreateFarmerProfileCommand> createValidator)
    {
        _mediator = mediator;
        _createValidator = createValidator;
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<FarmerProfileDto>> GetByUserId(Guid userId)
    {
        try
        {
            var query = new GetFarmerProfileQuery(userId);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound(new { message = "Farmer profile not found." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the profile." });
        }
    }

    [HttpPost]
    public async Task<ActionResult<FarmerProfileDto>> Create([FromBody] CreateFarmerProfileDto request)
    {
        try
        {
            var command = new CreateFarmerProfileCommand(
                request.UserId, // You'll need to get this from authentication context
                request.FullName,
                request.CompanyName,
                request.LegalForm,
                request.CompanyIdno,
                request.RegistrationDate,
                request.Email,
                request.Phone,
                request.Address,
                request.RepFirstName,
                request.RepLastName,
                request.RepDateOfBirth,
                request.RepGender,
                request.RepIdNumber,
                request.CropCultivation,
                request.AnimalHusbandry,
                request.MixedFarming,
                request.ProcessingActivities,
                request.TotalFarmland,
                request.Arable,
                request.Orchards,
                request.Vineyards,
                request.Pastures,
                request.Hayfields,
                request.Greenhouses,
                request.Cattle,
                request.Pigs,
                request.Sheep,
                request.Goats,
                request.Horses,
                request.Poultry,
                request.TotalEmployees,
                request.FamilyMembers,
                request.SeasonalWorkers,
                request.AnnualIncome,
                request.AnnualExpenses,
                request.SubsidiesReceived,
                request.Notes,
                request.ConsentDataSharing,
                request.AcceptPrivacyPolicy
            );

            var validationResult = await _createValidator.ValidateAsync(command);
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

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetByUserId), new { userId = result.UserId }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the profile." });
        }
    }
}
