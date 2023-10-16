

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();
app.UseStaticFiles();
app.UseMvc(
    r=>
    {
        r.MapRoute(
            name: "default",
            template: "{controller=Person}/{action=AllView}"
            );
    });

//app.MapGet("/", async (context) => await context.Response.WriteAsync("Hello World!"));

app.Run();
