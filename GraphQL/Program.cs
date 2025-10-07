using GraphQL.Mutations;
using GraphQL.Queries;
using Microsoft.EntityFrameworkCore;
using WatchTogether.Backend.GraphQL.Data;
using WatchTogether.Backend.GraphQL.Mutations;
using WatchTogether.Backend.GraphQL.Queries;
using WatchTogether.Backend.GraphQL.Subscriptions;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext (runtime DI)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=watchtogether.db"));

// Add GraphQL server
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutations>()
    .AddType<UserTypes>()
    .AddType<RoomTypes>()
    .AddType<MessageTypes>()
    .AddFiltering()
    .AddSorting()
     .AddSubscriptionType<MessageSubscriptions>()
     .AddInMemorySubscriptions();


// Register individual query classes
builder.Services.AddScoped<UserQueries>();
builder.Services.AddScoped<RoomQueries>();
builder.Services.AddScoped<MessageQueries>();

// Register the wrapper Query class
builder.Services.AddScoped<Query>();
builder.Services.AddScoped<UserMutations>();
builder.Services.AddScoped<RoomMutations>();
builder.Services.AddScoped<MessageMutations>();
builder.Services.AddScoped<Mutations>();
var app = builder.Build();

// Map GraphQL endpoint
app.MapGraphQL("/graphql");

// Optional health check
app.MapGet("/", () => "WatchTogether GraphQL API is running.");

// Ensure database is created (for dev/testing)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
