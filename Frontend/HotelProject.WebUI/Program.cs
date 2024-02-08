using FluentValidation;
using FluentValidation.AspNetCore;
using HotelProject.DataAccessLayer.Concrete;
using HotelProject.EntityLayer.Concrete;
using HotelProject.WebUI.Dtos.GuestDto;
using HotelProject.WebUI.ValidationRules.GuestValidationRules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(); // hata mesajý için
builder.Services.AddTransient<IValidator<CreateGuestDto>, CreateGuestValidator>();//bunuda eklemen lazým ki hata mesajý olsun
builder.Services.AddTransient<IValidator<UpdateGuestDto>, UpdateGuestValidator>();//bunuda eklemen lazým ki hata mesajý olsun

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpClient();
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
