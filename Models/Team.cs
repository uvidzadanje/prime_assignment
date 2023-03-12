using System.ComponentModel.DataAnnotations;

namespace Models;

public class Team: Model
{
    [MaxLength(50)]
    public string Name { get; set; }

    public List<Employee>? Employees { get; set; }

    public List<Models.Task>? Tasks { get; set; }
    
    public Employee? Leader { get; set; }
}