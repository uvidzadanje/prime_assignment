using System.ComponentModel.DataAnnotations;

namespace Dto;

public class TaskDto
{
    public int ID { get; set; }

    [MaxLength(60)]
    public string Title { get; set; }

    public string Description { get; set; }

    public int AssigneeID { get; set; }
    public DateTime DueDate { get; set; }

}