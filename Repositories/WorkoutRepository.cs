public class WorkoutRepository : IWorkoutRepository
{
    private readonly WorkoutContext _workoutContext;
    private readonly IExerciseRepository _exerciseRepository;

    public WorkoutRepository(WorkoutContext workoutDb, IExerciseRepository exerciseRepository)
    {
        _workoutContext = workoutDb;
        _exerciseRepository = exerciseRepository;
    }

    public void AddWorkout(Workout workout)
    {
        var workoutDb = new WorkoutDb(
            workout.Id, 
            workout.Title, 
            workout.Description
        );

        _workoutContext.Workouts.Add(workoutDb);
        Save();
    }

    public void DeleteWorkout(Workout workout)
    {
        var workoutDb = new WorkoutDb(
            workout.Id, 
            workout.Title, 
            workout.Description
        );

        _workoutContext.ChangeTracker.Clear();
        _workoutContext.Workouts.Remove(workoutDb);
        Save();
    }

    public Workout? GetWorkout(string id)
    {
        var existingWorkout = _workoutContext.Workouts.Find(id);

        if (existingWorkout == null){
            return null;
        }

        var workoutExercises = _exerciseRepository.GetWorkoutExercises(Int32.Parse(existingWorkout.Id));
        var exercises = workoutExercises.Select(x => new Exercise(x.Name, x.Sets, x.Reps, x.Duration)).ToList();

        var workout = new Workout(existingWorkout.Id, existingWorkout.Title, existingWorkout.Description, exercises);

        return workout;
    }

    private void Save()
    {
        _workoutContext.SaveChanges();
    }
}