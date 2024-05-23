using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkoutsManagement.Models.Client;
using WorkoutsManagement.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WorkoutContext>(opt => opt.UseInMemoryDatabase("WorkoutDB"));
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IWorkoutDateRepository, WorkoutDateRepository>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Enable swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "WorkoutAPI";
    config.Title = "WorkoutAPI v1";
    config.Version = "v1";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "WorkoutAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

// {
//     "id": "1",
//     "title": "Upper Body Workout",
//     "description": "Focuses on chest, shoulders, back, and arms.",
//     "exercises": [
//     {
//         "name": "Push-ups",
//         "sets": 3,
//         "reps": 10,
//         "duration": "10 minutes"
//     },
//     {
//         "name": "Pull-ups",
//         "sets": 3,
//         "reps": 8,
//         "duration": "15 minutes"
//     }
//     ]
// }

app.MapPost("/workout", (Workout newWorkout, IWorkoutRepository workoutRepository, IExerciseRepository exerciseRepository) =>
{
    var existingWorkout = workoutRepository.GetWorkout(newWorkout.Id);

    if (existingWorkout is Workout workout){
        return Results.BadRequest($"Workout with workout id {newWorkout.Id} already exists");
    }

    workoutRepository.AddWorkout(newWorkout);

    foreach(var exercise in newWorkout.Exercises)
    {
        exerciseRepository.AddExercise(exercise, int.Parse(newWorkout.Id));
    }

    return Results.Created($"/Workout/{newWorkout.Id}", newWorkout);
});

app.MapGet("/workout/{id}", (string id, IWorkoutRepository workoutRepository) =>
{
    return workoutRepository.GetWorkout(id) 
        is Workout workout
            ? Results.Ok(workout)
            : Results.NotFound();
});

app.MapDelete("/workout/{id}", (string id, IWorkoutRepository workoutRepository, IExerciseRepository exerciseRepository) =>
{
    var workoutToDelete = workoutRepository.GetWorkout(id);
    if (workoutToDelete is Workout workout)
    {
        workoutRepository.DeleteWorkout(workoutToDelete);
        exerciseRepository.DeleteExercises(int.Parse(workoutToDelete.Id));
        
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.MapGet("/workout/summary/{id}", (int id, IExerciseRepository exerciseRepository) =>
{
    var excercises = exerciseRepository.GetWorkoutExercises(id);

    if (excercises == null)
    {
        return Results.BadRequest($"No workouts were found under workout id {id}");
    }

    var workoutSummary = new WorkoutSummary
        {
            WorkoutId = id.ToString(),
            TotalReps = excercises.Sum(x => x.Reps),
            TotalSets = excercises.Sum(x => x.Sets),
            TotalDuration = "Not working"
        };

    return Results.Ok(workoutSummary);
});

// {
//     "date": "2024-04-01",
//     "workouts": [
//     {
//         "title": "Upper Body Workout",
//         "description": "Focuses on chest, shoulders, back, and arms.",
//         "summary": {
//             "totalSets": 6,
//             "totalReps": 34,
//             "totalDuration": "25 minutes"
//         },
//         "exercises": [
//         {
//             "name": "Push-ups",
//             "sets": 3,
//             "reps": 10,
//             "duration": "10 minutes"
//         },
//         {
//             "name": "Pull-ups",
//             "sets": 3,
//             "reps": 8,
//             "duration": "15 minutes"
//         }
//         ]
//     }
//     ]
// }

app.MapPost("/workout/date", (string workoutId, DateTime date, IWorkoutRepository workoutRepository, IWorkoutDateRepository workoutDateRepository) =>
{
    var existingWorkout = workoutRepository.GetWorkout(workoutId);

    if (existingWorkout == null){
        return Results.BadRequest($"Workout with workout id {workoutId} does not exist");
    }
    
    DateTime temp;
    if(DateTime.TryParse(date.ToString(), out temp))
    {
        workoutDateRepository.AddWorkoutDate(temp, workoutId);
    }
    else
    {
        Results.BadRequest("Invalid date format");
    }
    
    var workoutWithDate = new WorkoutWithDate(date, new List<Workout> { existingWorkout});

    return Results.Created($"/workout/date", workoutWithDate);
});

app.Run();