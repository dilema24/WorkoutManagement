using Microsoft.EntityFrameworkCore;
using WorkoutsManagement.Models.Database;

public class WorkoutContext : DbContext
{
    public WorkoutContext(DbContextOptions<WorkoutContext> options)
        : base(options) { }

    public DbSet<WorkoutDb> Workouts { get; set; }
    public DbSet<ExerciseDb> Exercises { get; set; }
    
    public DbSet<WorkoutDatesDb> WorkoutDates { get; set; }
}