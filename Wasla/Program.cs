using Hangfire;
using Hangfire.Dashboard;
using HangfireBasicAuthenticationFilter;
using Wasla;
using Wasla.Common.Caching;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.TagActionsBy(api =>
    {
        var method = api.HttpMethod;
        return [method ?? "OTHER"];
    });

    c.DocInclusionPredicate((_, _) => true);
});

builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<ICacheService, CacheService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHangfireDashboard("/jobs", new DashboardOptions
{
    Authorization =
    [
        new HangfireCustomBasicAuthenticationFilter
        {
            User = app.Configuration.GetValue<string>("HangfireSettings:Username"),
            Pass = app.Configuration.GetValue<string>("HangfireSettings:Password")
        }
    ],
    DashboardTitle = "Wasla Dashboard",
    IsReadOnlyFunc = (DashboardContext conext) => true
});

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

//using (var scope = app.Services.CreateScope())
//{
//    var jobs = scope.ServiceProvider.GetRequiredService<BackgroundJobs>();
//    jobs.RegisterRecurringJobs();
//}

app.UseStaticFiles();

app.UseExceptionHandler();

//app.UseRateLimiter(); // Rate Limiting Middleware 

app.MapControllers(); // Map controller routes. it differs from MVC pattern 


app.Run();