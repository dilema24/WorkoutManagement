public interface IExerciseRepository
{
    void DeleteExercises(int workoutId);
    void AddExercise(Exercise exercise, int workoutId);
    IReadOnlyCollection<ExerciseDb> GetWorkoutExercises(int workoutId);
}