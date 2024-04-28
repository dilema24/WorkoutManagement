using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ExerciseDb
{
    public ExerciseDb(int workoutId, string name, int sets, int reps, string duration)
    {
        WorkoutId = workoutId;
        Name = name;
        Sets = sets;
        Reps = reps;
        Duration = duration;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set;}
    [Required]
    public int WorkoutId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Sets { get; set; }
    [Required]
    public int Reps { get; set; }
    [Required]
    public string Duration { get; set; }
    
}