using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace prime_assignment.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamController : ApplicationController, ICRUDController<Team>
{
    public PrimeContext context { get; set; }

    public TeamController(PrimeContext context,
                          ILogger<TaskController> logger)
    :base(logger)
    {
        this.context = context;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Team obj)
    {
        try
        {
            context.Teams.Add(obj);
            await context.SaveChangesAsync();

            return OkResponse(new {
                message = "Team successfully added!",
                team = obj
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
            var team = await context
                .Teams
                .Where(team => team.ID == id)
                .Include(team => team.Leader)
                .Include(team => team.Employees)
                .Include(team => team.Tasks)
                .FirstOrDefaultAsync(); 
            
            return Ok(team);
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] Team obj)
    {
        try
        {
            context.Teams.Update(obj);
            await context.SaveChangesAsync();

            return OkResponse(new {
                message = "Team updated successfully!"
            });
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }

    [Route("id")]
    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var team = context.Teams.Where(team => team.ID == id).FirstOrDefault();

            if(team == null)
            {
                return BadRequestResponse(new {
                    message = "Team does not exist in database!"
                }); 
            }

            context.Teams.Remove(team);
            await context.SaveChangesAsync();

            return OkResponse(new {
                message = "Successfully deleted team!"
            });
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }

    [Route("{id}/employee/{employeeID}")]
    [HttpPost]
    public async Task<ActionResult> AddEmployeeToTeam(int id, int employeeID)
    {
        try
        {
            var team = context.Teams.Where(team => team.ID == id).Include(team => team.Employees).FirstOrDefault();

            if(team == null)
            {
                return BadRequestResponse(new {
                    message = "Team does not exist in database!"
                });
            }

            var employee = context.Employees.Where(employee => employee.ID == employeeID).FirstOrDefault();

            if(employee == null)
            {
                return BadRequestResponse(new {
                    message = "Employee does not exist in database!"
                });
            }

            team.Employees?.Add(employee);
            context.Teams.Update(team);
            await context.SaveChangesAsync();

            return OkResponse(new {
                message = "Successfully added employee to team!"
            });
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }

    [Route("{id}/employee/{employeeID}")]
    [HttpDelete]
    public async Task<ActionResult> DeleteEmployeeFromTeam(int id, int employeeID)
    {
        try
        {
            var team = context.Teams.Where(team => team.ID == id).FirstOrDefault();

            if(team == null)
            {
                return BadRequestResponse(new {
                    message = "Team does not exist in database!"
                });
            }

            var employee = context.Employees.Where(employee => employee.ID == employeeID).FirstOrDefault();

            if(employee == null)
            {
                return BadRequestResponse(new {
                    message = "Employee does not exist in database!"
                });   
            }

            team.Employees?.Remove(employee);
            context.Teams.Update(team);
            await context.SaveChangesAsync();

            return OkResponse(new {
                message = "Successfully removed employee from team!"
            });
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }

    [Route("{id}/leader/{employeeID}")]
    [HttpPut]
    public async Task<ActionResult> SetLeader(int id, int employeeID)
    {
        try
        {
            var team = context.Teams.Where(team => team.ID == id).FirstOrDefault();

            if(team == null)
            {
                return BadRequestResponse(new {
                    message = "Team does not exist in database!"
                });
            }

            var employee = new Employee() {ID = employeeID};

            team.Leader = employee;
            context.Employees.Attach(employee);
            context.Teams.Update(team);
            await context.SaveChangesAsync();

            return OkResponse(new {
                message = "Leader is set!"
            });
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }

    [Route("EmployeesPerTeam")]
    [HttpGet]
    public async Task<ActionResult> EmployeesPerTeam()
    {
        try
        {
            var EmployeesPerTeam = await context
                                .Teams
                                .Select(team => new {
                                    ID = team.ID,
                                    Name = team.Name,
                                    NumberOfEmployees = team.Employees.Count
                                })
                                .ToListAsync();

            return OkResponse(new {
                EmployeesPerTeam
            });
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }

    [Route("AvgSalaryPerTeam")]
    [HttpGet]
    public async Task<ActionResult> AvgSalaryPerTeam()
    {
        try
        {
            var EmployeesPerTeam = await context
                                .Teams
                                .Select(team => new {
                                    ID = team.ID,
                                    Name = team.Name,
                                    AvgSalary = team.Employees.Average(employee => employee.MonthSalary)
                                })
                                .ToListAsync();

            return OkResponse(new {
                EmployeesPerTeam
            });
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }

    [Route("{id}/task/{taskID}")]
    [HttpPost]
    public async Task<ActionResult> AddTaskToTeam(int id, int taskID)
    {
        try
        {
            var team = context.Teams.Where(team => team.ID == id).Include(team => team.Tasks).FirstOrDefault();

            if(team == null)
            {
                return BadRequestResponse(new {
                    message = "Team does not exist in database!"
                });
            }

            var task = context.Tasks.Where(task => task.ID == taskID).FirstOrDefault();

            if(task == null)
            {
                return BadRequestResponse(new {
                    message = "Task does not exist in database!"
                });
            }

            team.Tasks?.Add(task);
            context.Teams.Update(team);
            await context.SaveChangesAsync();

            return OkResponse(new {
                message = "Successfully added task to team!"
            });
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }
}
