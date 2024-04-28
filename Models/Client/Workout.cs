using System.ComponentModel.DataAnnotations;

public class Workout
{
    public Workout(string id, string title, string description, IReadOnlyCollection<Exercise> exercises)
    {
        Id = id;
        Title = title;
        Description = description;
        Exercises = exercises;
    }

    [Key]
    public string Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public IReadOnlyCollection<Exercise> Exercises { get; }
}