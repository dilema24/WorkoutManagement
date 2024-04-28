using Microsoft.EntityFrameworkCore;

public class WorkoutContext : DbContext
{
    public WorkoutContext(DbContextOptions<WorkoutContext> options)
        : base(options) { }

    public DbSet<WorkoutDb> Workouts { get; set; }
    public DbSet<ExerciseDb> Exercises { get; set; }
}