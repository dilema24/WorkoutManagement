using System.ComponentModel.DataAnnotations;

public class WorkoutSummary
{
    [Required]
    public string WorkoutId { get; set; }
    public int TotalSets { get; set; }
    public int TotalReps { get; set; }
    public string TotalDuration { get; set; }
}