using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.BackgroundServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "AirQuality API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient();
builder.Services.AddHttpClient("AirQualityAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["AirQualityAPI:BaseUrl"]);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<Context>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IAirQualityService, AirQualityService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IFavouriteCitiesService, FavouriteCitiesService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, DummyEmailSender>();
builder.Services.AddHttpContextAccessor();

var taskCompletionSource = new TaskCompletionSource<bool>();

builder.Services.AddSingleton(taskCompletionSource);
builder.Services.AddHostedService<ImportCities>();
builder.Services.AddHostedService<AirQualityBackgroundService>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.InjectStylesheet("/swagger-ui/custom.css");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});
app.MapRazorPages();

app.Run();
