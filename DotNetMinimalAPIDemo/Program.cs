using DotNetMinimalAPIDemo.RouterClasses;
using DotNetMinimalAPIDemo.Components;


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

// Add "Router" classes as a service
builder.Services.AddScoped<RouterBase, ProductRouter>();
builder.Services.AddScoped<RouterBase, CustomerRouter>();

var app = builder.Build();

//Use Cors need NuGet Package for it.
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("https://localhost:5296", "http://localhost:64714"));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//*************************************
// Add Routes from all "Router Classes" folder 
//*************************************
using (var scope = app.Services.CreateScope())
{
    // Instance of services where you build all RouterBase classes
    var services = scope.ServiceProvider.GetServices<RouterBase>();
    
    // Loop through each RouterBase class
    foreach (var item in services)
    {
        // Invoke the AddRoutes() method for each RouterBase class
        item.AddRoutes(app);
    }
    
    // Make sure this is called within the application scope
    app.Run();
}
