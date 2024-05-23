namespace WorkoutsManagement.Models.Client;

public class WorkoutWithDate
{
    public WorkoutWithDate(DateTime date, IReadOnlyCollection<Workout> workouts)
    {
        Date = date;
        Workouts = workouts;
    }

    public DateTime Date { get; set; }

    public IReadOnlyCollection<Workout> Workouts { get; set; }
}