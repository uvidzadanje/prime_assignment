using Microsoft.AspNetCore.Mvc;
using Models;

namespace prime_assignment.Controllers;
public interface ICRUDController<T>
{
    Task<ActionResult> Create(T obj);
    Task<ActionResult> Read(int id);
    Task<ActionResult> Update(T obj);
    Task<ActionResult> Delete(int id);
}