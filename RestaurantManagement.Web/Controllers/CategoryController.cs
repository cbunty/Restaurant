using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;
using System.Text;

namespace CategoryManagement.Web.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            PagedResults<CategoryResponseModel> categoryList = new PagedResults<CategoryResponseModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7123/api/Category"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        categoryList = JsonConvert.DeserializeObject<PagedResults<CategoryResponseModel>>(apiResponse);
                    }
                }
            }
            return View(categoryList);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryResponseModel reqModel)
        {
            CategoryResponseModel category = new CategoryResponseModel();
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("username")))
            {
                using (var httpClient = new HttpClient())
                {
                    var requestModel = new CategoryRequestModel()
                    {
                        Description = reqModel.Description,
                        Name = reqModel.Name,
                        UserId = HttpContext.Session.GetString("username")
                    };

                    using (var response = await httpClient.PostAsync("https://localhost:7123/api/Category", new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json")))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            category = JsonConvert.DeserializeObject<CategoryResponseModel>(apiResponse);
                            TempData["ResultOk"] = "Record Added Successfully !";
                            return RedirectToAction("Index");
                        }
                    }
                }

            }

            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CategoryResponseModel category = new CategoryResponseModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7123/api/Category/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        category = JsonConvert.DeserializeObject<CategoryResponseModel>(apiResponse);
                        if (category == null)
                        {
                            return NotFound();
                        }
                    }
                }
            }

            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryResponseModel reqObj)
        {
            CategoryResponseModel category = new CategoryResponseModel();
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("username")))
            {
                
                var requestModel = new CategoryRequestModel()
                {
                    Id = reqObj.Id,
                    Description = reqObj.Description,
                    Name = reqObj.Name,
                    UserId = HttpContext.Session.GetString("username")
                };
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsync($"https://localhost:7123/api/Category/{reqObj.Id}", new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json")))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            category = JsonConvert.DeserializeObject<CategoryResponseModel>(apiResponse);
                            TempData["ResultOk"] = "Data Updated Successfully !";
                            return RedirectToAction("Index");
                        }
                    }
                }

            }

            return View(category);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:7123/api/Category/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["ResultOk"] = "Data Deleted Successfully !";
                        return RedirectToAction("Index");
                    }
                }
            }
            return NotFound();
        }
    }
}
