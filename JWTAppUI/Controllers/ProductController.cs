using JWTAppUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace JWTAppUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();

            var token = User.Claims.SingleOrDefault(x => x.Type == "accessToken").Value;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Artık token bilgisini aldım, isteklerimi şekillendirebilirim.

            return client;
        }
        public async Task<IActionResult> List()
        {
            var client = CreateClient();

            var respone = await client.GetAsync("http://localhost:5222/api/Products");
            if (respone.IsSuccessStatusCode)
            {
                var jsonString = await respone.Content.ReadAsStringAsync();
                var list = JsonSerializer.Deserialize<List<ProductListRepsonseModel>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });


                foreach (var item in list)
                {
                    var responseCat = await client.GetAsync("http://localhost:5222/api/Categories/" + item.CategoryId);

                    var categoryJsonString = await responseCat.Content.ReadAsStringAsync();

                    var cat = JsonSerializer.Deserialize<CategoryListResponseModel>(categoryJsonString, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });

                    item.Category = cat;
                }
                return View(list);

            }

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var client = this.CreateClient();

            await client.DeleteAsync($"http://localhost:5222/api/Products/{id}");

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Create()
        {
            var client = this.CreateClient();

            var response = await client.GetAsync("http://localhost:5222/api/Categories");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = JsonSerializer.Deserialize<List<CategoryListResponseModel>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

                ViewBag.Categories = new SelectList(list, "Id", "Definition");

                return View();
            } return RedirectToAction("List");

        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequestModel model)
        {
            if(ModelState.IsValid)
            {
                var client = CreateClient();

                var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var repsonse = await client.PostAsync("http://localhost:5222/api/Products", requestContent);

                return RedirectToAction("List");
            }

            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var client = this.CreateClient();

            var response = await client.GetAsync("http://localhost:5222/api/Products/" + id);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var productModel = JsonSerializer.Deserialize<ProductUpdateRequestModel>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

                var responseCat = await client.GetAsync("http://localhost:5222/api/Categories");

                if (responseCat.IsSuccessStatusCode)
                {
                    var catJsonString = await responseCat.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<CategoryListResponseModel>>(catJsonString, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });

                    ViewBag.Categories = new SelectList(list, "Id", "Definition");

                    return View(productModel);
                }
                return RedirectToAction("List");
            }
            return RedirectToAction("List");

        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var client = CreateClient();

                var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var repsonse = await client.PutAsync("http://localhost:5222/api/Products", requestContent);

                return RedirectToAction("List");
            }

            return View(model);
        }
    }
}
