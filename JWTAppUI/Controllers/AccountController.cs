using JWTAppUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace JWTAppUI.Controllers
{
    public class AccountController : Controller
    {
        //Api ile iletişim kurabilmek için IHttpClientFactory kullanıyorum,
        //bunun sayesinde bir client oluşturup, auth controller içerisindeki login işlemlerimi gerçekleştirebiliyorum.
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginModel model)
        {
            //İsteği atacak bir client oluşturulur
            var client = _httpClientFactory.CreateClient();

            //İçerik json formatına dönüştürülür, bunu eskilerdiki gibi Newtonsoft.Json yerine microsftun kendi kütüphanesini kullanabiliriz.
            var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            //pust url'in nereye gideceğini ve contentini ona veriyorum
            var response = await client.PostAsync("http://localhost:5222/api/Auth/SignIn", requestContent);

            if(response.IsSuccessStatusCode)
            {
                //başarılı olan response'un contenti okunur.
                var jsonData = await response.Content.ReadAsStringAsync();
                var tokenModel = JsonSerializer.Deserialize<JWTResponeModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,//null gelmemesi için namingPolicy düzenlemesi gerçekleştiriyorum.
                });

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(tokenModel.Token);//tokenın direk kendisini yakaladım

                if(token != null)
                {

                    var roles = token.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value); //rol bilgilerini alırım;

                    //if(roles.Contains("Admin"))
                    //{
                    //    //redirect
                    //}

                    var claims = token.Claims.ToList(); //Claimslerimi liste haline getiriyorunki, token'ı frontendden giden isterkler içerisine ekleyebileyim, daha sonra ekleme şlemini gerçekleştiriyorum (eğer null değilse)
                    claims.Add(new Claim("accessToken", tokenModel.Token == null ? "" : tokenModel.Token));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

                    var authProps = new AuthenticationProperties
                    {
                        AllowRefresh = false, //refreshlenmerde token bitiş süresini yenilemez
                        ExpiresUtc = tokenModel.ExpireDate,
                        IsPersistent = true, //token süresi bitmediği sürece hatırlansın
                    };

                    await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProps);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya Şifre yanlış");

                    return View(model);
                }
            }
            else
            {
                //burada else if ler ile diğer status codelar kontrol edilebilir, ben şimdilik kısa tutuyorum.
                ModelState.AddModelError("", "Kullanıcı adı veya Şifre yanlış");
                return View(model);
            }
        }
    }
}
