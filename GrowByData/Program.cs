using GrowByData.Data;
using GrowByData.Data.Entities;
using GrowByData.Job;
using GrowByData.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<GrowByDataDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);
builder.Services.AddScoped<IDapperr, Dapperr>();
builder.Services.AddScoped<IGrowByDataService, GrowByDataService>();
builder.Services.AddSignalR();

//Add Quartz Schedular to the services
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();


    var nonConconcurrentJobKey = new JobKey("FetchingProductFromApi");
    q.AddJob<FetchingProductFromApi>(opts => opts.WithIdentity(nonConconcurrentJobKey));
    q.AddTrigger(opts => opts
        .ForJob(nonConconcurrentJobKey)
        .WithIdentity("NonConconcurrentJob-trigger")
        .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(300)
            .RepeatForever()));

});

builder.Services.AddQuartzHostedService(
    q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
