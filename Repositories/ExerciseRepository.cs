public class ExerciseRepository : IExerciseRepository
{
    private readonly WorkoutContext _workoutContext;

    public ExerciseRepository(WorkoutContext workoutContext)
    {
        _workoutContext = workoutContext;
    }

    public void AddExercise(Exercise exercise, int workoutId)
    {
        var exerciseDb = new ExerciseDb(
            workoutId, 
            exercise.Name, 
            exercise.Sets, 
            exercise.Reps, 
            exercise.Duration);

        _workoutContext.Exercises.Add(exerciseDb);
        Save();
    }

    public void DeleteExercises(int workoutId)
    {
        var workoutExcercises = GetWorkoutExercises(workoutId);

        foreach(var exercise in workoutExcercises){
            _workoutContext.ChangeTracker.Clear();
            _workoutContext.Exercises.Remove(exercise);
            Save();
        }
    }

    public IReadOnlyCollection<ExerciseDb> GetWorkoutExercises(int workoutId)
    {
        return _workoutContext.Exercises
                .Where(x => x.WorkoutId == workoutId)
                .Select(x => x)
                .ToList();
    }

    private void Save()
    {
        _workoutContext.SaveChanges();
    }
}