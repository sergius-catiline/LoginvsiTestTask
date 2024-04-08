using LoginvsiTestTask.Models;
using LoginvsiTestTask.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace LoginvsiTestTask.Startup;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddAutoMapper(typeof(Startup));
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoginVSI API", Version = "v1" }); });
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAssignmentService, AssignmentService>();
        services.AddTransient<IAssignUserService, AssignUserService>();
        services.AddSingleton<IRandomService, RandomService>();
        services.AddHostedService<TimerTaskAssignmentService>();
        services.AddDbContext<AssignmentDbContext>(options => 
            options.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }))
                   .UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger(); 
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", " V1"); }); 
        }

        app.UseRouting();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}