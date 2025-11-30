using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Services;
using Infrastructure.Services.BackgroundServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Models.IServices;

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

//builder.Services.AddHttpClient("OpenAqAPI", client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["OpenAqAPI:BaseUrl"]);
//    client.DefaultRequestHeaders.Add("X-API-Key", builder.Configuration["OpenAqAPI:ApiKey"]);
//});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<Context>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IAirQuality, AirQualityService>();
builder.Services.AddScoped<ICity, CityService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.MapRazorPages();
//using (var scope = app.Services.CreateScope())
//{
//    var cityService = scope.ServiceProvider.GetRequiredService<ICity>();
//    cityService.ImportCitiesInDB();
//}


app.Run();
