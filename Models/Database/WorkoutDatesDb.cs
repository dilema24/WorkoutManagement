using System.ComponentModel.DataAnnotations;

namespace WorkoutsManagement.Models.Database;

public class WorkoutDatesDb
{
    public WorkoutDatesDb(string id, DateTime date)
    {
        Id = id;
        Date = date;
    }

    [Key]
    public string Id { get; set;}
    [Required]
    public DateTime Date { get; set; }
}