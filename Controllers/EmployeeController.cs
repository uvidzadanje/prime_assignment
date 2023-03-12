using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace prime_assignment.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ApplicationController, ICRUDController<Employee>
{
    private PrimeContext context { get; set; }

    public EmployeeController(PrimeContext context,
                              ILogger<EmployeeController> logger)
    :base(logger)
    {
        this.context = context;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Employee obj)
    {
        try
        {
            context.Employees.Add(obj);
            await context.SaveChangesAsync();

            return OkResponse(new {
                message = "Employee successfully added!",
                employee = obj
            });
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }

    [Route("{id}")]
    [HttpGet]
    public async Task<ActionResult> Read(int id)
    {
        try
        {
            var employee = await context
                .Employees
                .Where(employee => employee.ID == id)
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFoundResponse();
            }

            return OkResponse(employee);
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] Employee obj)
    {
        try
        {
            context.Employees.Update(obj);
            await context.SaveChangesAsync();

            return OkResponse(new {
                message = "Employee updated successfully!"
            });
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var employee = context.Employees.Where(employee => employee.ID == id).FirstOrDefault();

            if(employee == null) 
            { 
                return BadRequestResponse(new {
                    message = "Employee does not exist in database!"
                });
            }

            context.Employees.Remove(employee);
            await context.SaveChangesAsync();

            return OkResponse(new {
                message = "Successfully deleted employee!"
            });
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }

    [Route("stats")]
    [HttpGet]
    public async Task<ActionResult> Stats()
    {
        try
        {
            var averageMonthSalary = await context
                        .Employees
                        .AverageAsync(employee => employee.MonthSalary);

            return OkResponse(new {
                avg_salary = averageMonthSalary
            });
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }
}
