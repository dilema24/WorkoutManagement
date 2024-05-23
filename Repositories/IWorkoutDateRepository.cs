namespace WorkoutsManagement.Repositories;

public interface IWorkoutDateRepository
{
    void AddWorkoutDate(DateTime date, string workoutId);
    DateTime? GetWorkoutDate(string workoutId);
}