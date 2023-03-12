using Microsoft.AspNetCore.Mvc;
using Models;

namespace prime_assignment.Controllers;

public class ApplicationController: ControllerBase
{
    protected readonly ILogger logger;

    public ApplicationController(ILogger logger)
    {
        this.logger = logger;
    }

    protected ObjectResult InternalServerErrorResponse(Exception ex)
    {
        logger.LogError(0, ex, "Unexpected error occured!");

        return StatusCode(500, new {
            success = false,
            error = "Unxepected error occured!"
        });
    }

    protected OkObjectResult OkResponse(Object data)
    {
        return Ok(new {
            success = true,
            data
        });
    }

    protected BadRequestObjectResult BadRequestResponse(Object data)
    {
        return BadRequest(new {
            success = false,
            data
        });
    }

    protected ObjectResult NotFoundResponse()
    {
        return NotFound(new {
            success = false,
            error = "Data not found!"
        });
    }
}