var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseStaticFiles();
app.UseMvc();


app.Run();
