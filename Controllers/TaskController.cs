using Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace prime_assignment.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ApplicationController, ICRUDController<TaskDto>
{
    private PrimeContext context { get; set; }

    public TaskController(PrimeContext context,
                          ILogger<TaskController> logger)
    :base(logger)
    {
        this.context = context;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] TaskDto obj)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var task = new Models.Task() {
                Description = obj.Description,
                Title = obj.Title,
                DueDate = obj.DueDate
            };
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var employee = context.Employees.Where(employee => employee.ID == obj.AssigneeID).FirstOrDefault();

            if (employee == null)
            {
                transaction.Rollback();
                return BadRequestResponse(new {
                    error = "Employee with that ID does not exist!"
                });
            }

            task.Assignee = employee;
            context.Tasks.Update(task);
            await context.SaveChangesAsync();

            transaction.Commit();
            
            return OkResponse(new {
                message = "Task successfully added!",
                task = obj
            });
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return InternalServerErrorResponse(ex);
        }
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var task = context.Tasks.Where(task => task.ID == id).FirstOrDefault();

            if(task == null)
            {
                return BadRequestResponse(new {
                    message = "Task does not exist in database!"
                });
            }

            context.Tasks.Remove(task);
            await context.SaveChangesAsync();

            return OkResponse(new {
                message = "Successfully deleted task!"
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
            var task = await context
                .Tasks
                .Where(task => task.ID == id)
                .Include(task => task.Assignee)
                .FirstOrDefaultAsync();

            if (task == null)
            {
                return NotFoundResponse();
            }

            return OkResponse(task);
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }

    [HttpPut]
    public async Task<ActionResult> Update(TaskDto obj)
    {
        try
        {
            var task = context.Tasks.Where(task => task.ID == obj.ID).FirstOrDefault();

            if(task == null)
            {
                return BadRequestResponse(new {
                    error = "Task does not exist in database!"
                });
            }

            task.Title = obj.Title;
            task.Description = obj.Description;
            task.DueDate = obj.DueDate;
            task.Assignee = context.Employees.Where(employee => employee.ID == obj.AssigneeID).FirstOrDefault();

            context.Tasks.Update(task);
            await context.SaveChangesAsync();

            return OkResponse(new {
                message = "Task updated successfully!"
            });
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse(ex);
        }
    }
}