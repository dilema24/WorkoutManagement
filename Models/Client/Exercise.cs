using System.ComponentModel.DataAnnotations;

public class Exercise
{
    public Exercise(string name, int sets, int reps, string duration)
    {
        Name = name;
        Sets = sets;
        Reps = reps;
        Duration = duration;
    }

    [Required]
    public string Name { get; set; }
    [Required]
    public int Sets { get; set; }
    [Required]
    public int Reps { get; set; }
    [Required]
    public string Duration { get; set; }
    public Workout Workout { get; set;}
}

