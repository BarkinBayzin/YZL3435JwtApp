using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//İlgili controller endpointleri yakalamak için HttpClient ekliyorum (API)
builder.Services.AddHttpClient();

//Token ayarlamaları gerçekleştirilir.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    //hangi path ile login olur, sisteme tanıtıyorum
    opt.LoginPath = "/Account/SignIn";
    opt.LogoutPath = "/Account/Logout"; //logout url'i
    opt.AccessDeniedPath = "/Account/AccessDenied"; //eğer kullanıcının yetkisi yoksa
    opt.Cookie.SameSite = SameSiteMode.Strict; //sadece o domainde kullansın diye
    opt.Cookie.HttpOnly = true; //diyerek js saldırılarından koruyorum
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; //ssl ayarlaması cookie için
    opt.Cookie.Name = "JwtCookie";
}); //cookie ayarlamaları artık tamam, sıra okumada..

var app = builder.Build();

//Routing ve endpoint middlewarelarımı çağırıyorum
app.UseRouting();

//middeleware çağırılma sırasına dikkat ediyorum.
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
