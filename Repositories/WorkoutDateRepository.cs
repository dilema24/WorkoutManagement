using WorkoutsManagement.Models.Database;

namespace WorkoutsManagement.Repositories;

public class WorkoutDateRepository : IWorkoutDateRepository
{
    private readonly WorkoutContext _workoutContext;
    
    public WorkoutDateRepository(WorkoutContext workoutContext)
    {
        _workoutContext = workoutContext;
    }
    
    public void AddWorkoutDate(DateTime date, string workoutId)
    {
        var workoutDate = new WorkoutDatesDb(workoutId, date);
        _workoutContext.WorkoutDates.Add(workoutDate);
    }
    
    public DateTime? GetWorkoutDate(string workoutId)
    {
        var workoutDate = _workoutContext.WorkoutDates.Find(workoutId);

        return workoutDate?.Date;
    }
}