using FluentValidation;
using FluentValidation.AspNetCore;
using HotelProject.DataAccessLayer.Concrete;
using HotelProject.EntityLayer.Concrete;
using HotelProject.WebUI.Dtos.GuestDto;
using HotelProject.WebUI.ValidationRules.GuestValidationRules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(); // hata mesajý için
builder.Services.AddTransient<IValidator<CreateGuestDto>, CreateGuestValidator>();//bunuda eklemen lazým ki hata mesajý olsun
builder.Services.AddTransient<IValidator<UpdateGuestDto>, UpdateGuestValidator>();//bunuda eklemen lazým ki hata mesajý olsun

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpClient();
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();
//builder.Services.AddMvc(config =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//    .RequireAuthenticatedUser()//otantike olmasý için kullanýcý giriþi zorunlu
//    .Build();
//    config.Filters.Add(new AuthorizeFilter(policy));
//});
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.Cookie.HttpOnly= true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);//10 dakika te otantike olacak
//    options.LoginPath = "/Login/Index";
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();//use otantikeþýn
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
