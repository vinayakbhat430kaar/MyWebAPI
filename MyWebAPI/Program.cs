using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MyWebAPI.Data;
using MyWebAPI.Repository;

internal class Program
{
    public IConfiguration Config{ get; }
    public Program(IConfiguration configuration)
    {
        Config = configuration;
    }


    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        var config = builder.Configuration;
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers().AddNewtonsoftJson();
        Console.WriteLine(config.GetConnectionString("testDB"));
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddDbContext<EmployeesContext>(options => options.UseSqlServer(config.GetConnectionString("testDB")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseDeveloperExceptionPage();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseRouting();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); }) ;

        app.Run();
    }
}