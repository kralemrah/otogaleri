using BusinessLogic.Abstract;
using BusinessLogic.Concrete;
using Data;
using Hangfire;
using Malikinden.Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MalikindenDbContext>();

builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));
builder.Services.AddTransient<ICarService,CarService>();

builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
	x.LoginPath = "/Admin/Login";
	x.AccessDeniedPath = "/AccessDenied";
	x.LogoutPath = "/Admin/Logout";
	x.Cookie.Name = "Admin";
	x.Cookie.MaxAge = TimeSpan.FromDays(7);
	x.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization(x =>
{
	x.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Role","Admin"));
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim("Role", "User"));
   

});
// Configure Hangfire
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(@"Server=EMRAH\SQLEXPRESS;Database=DbMalikinden;Trusted_Connection=True;TrustServerCertificate=True"));
builder.Services.AddHangfireServer();

var app = builder.Build();
// Use Hangfire Dashboard (optional for monitoring jobs)
app.UseHangfireDashboard("/hangfire");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
			name: "admin",
			pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
		  );

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

//RecurringJob.AddOrUpdate(
//    "my-recurring-job",                      // Job identifier
//    () => Console.WriteLine("Hello, Hangfire!"), // The method to execute
//    "* * * * *"                              // Cron expression for every 1 minute
//);

// Periodic job setup (1-minute interval job)
//RecurringJob.AddOrUpdate(
//    "my-recurring-job3",  // Job identifier (optional)
//() => Halper.EpostaGonder(app.Services),
//    Cron.Minutely);  // This cron expression runs every minute

//Enqueue a background job from Program.cs

// Enqueue a recurring job that runs every minute
EnqueueRecurringJob(app.Services);

app.Run();
void EnqueueRecurringJob(IServiceProvider services)
{
	// Create a scope to resolve services from DI container
	using (var scope = services.CreateScope())
	{
		var backgroundJobClient = scope.ServiceProvider.GetRequiredService<IBackgroundJobClient>();

		// Schedule a recurring job to run every minute
		RecurringJob.AddOrUpdate<MyBackgroundJob>(
			job => job.ProcessDataAsync(), // The method to run
			"*/1 * * * *" // Cron expression for every minute
			);
	}
}

//class Halper 
//{
//    public static void EpostaGonder(IServiceProvider? provider)
//    {
//        // Create a scope to seed data
//        using (var scope = provider.CreateScope())
//        {
//            var dbContext = scope.ServiceProvider.GetRequiredService<MalikindenDbContext>();
//            dbContext.Markalar.Add(new Entity.Marka() { Adi = DateTime.Now.ToString() });
//            dbContext.SaveChanges();
//        }
//    }
//}
