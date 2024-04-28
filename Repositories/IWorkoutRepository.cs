public interface IWorkoutRepository
{
    Workout? GetWorkout(string id);
    void AddWorkout(Workout workout);
    void DeleteWorkout(Workout workout);
}