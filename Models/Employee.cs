using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;
public class Employee: Model
{
    [MaxLength(50)]
    [MinLength(3)]
    public string FullName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string PhoneNumber { get; set; }

    public DateTime DateOfBirth { get; set; }
    
    public decimal MonthSalary { get; set; }
}