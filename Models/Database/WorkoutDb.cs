using System.ComponentModel.DataAnnotations;
public class WorkoutDb
{
    public WorkoutDb(string id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }

    [Key]
    public string Id { get; set;}
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
}