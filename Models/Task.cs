using System.ComponentModel.DataAnnotations;

namespace Models;
public class Task: Model
{
    [MaxLength(60)]
    public string Title { get; set; }

    public string Description { get; set; }

    public Employee? Assignee { get; set; }
    
    public DateTime DueDate { get; set; }
}