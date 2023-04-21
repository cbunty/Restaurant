using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;
using System.Text;

namespace RestaurantManagement.Web.Controllers
{
    public class RestaurantController : Controller
    {
        public async Task<IActionResult> Index()
        {
            PagedResults<RestaurantResponseModel> restaurantList = new PagedResults<RestaurantResponseModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7123/api/Restaurant"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        restaurantList = JsonConvert.DeserializeObject<PagedResults<RestaurantResponseModel>>(apiResponse);
                    }
                }
            }
            return View(restaurantList);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RestaurantResponseModel reqModel)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("username")))
            {
                RestaurantResponseModel restaurant = new RestaurantResponseModel();
                using (var httpClient = new HttpClient())
                {
                    var requestModel = new RestaurantRequestModel()
                    {
                        Address = reqModel.Address,
                        Description = reqModel.Description,
                        Name = reqModel.Name,
                        PhoneNumber = reqModel.PhoneNumber,
                        WebsiteUrl = reqModel.WebsiteUrl,
                        UserId = HttpContext.Session.GetString("username")
                    };

                    using (var response = await httpClient.PostAsync("https://localhost:7123/api/Restaurant", new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json")))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            restaurant = JsonConvert.DeserializeObject<RestaurantResponseModel>(apiResponse);
                            TempData["ResultOk"] = "Record Added Successfully !";
                            return RedirectToAction("Index");
                        }
                    }
                }

            }

            return View(reqModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            RestaurantResponseModel restaurant = new RestaurantResponseModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7123/api/Restaurant/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        restaurant = JsonConvert.DeserializeObject<RestaurantResponseModel>(apiResponse);
                        if (restaurant == null)
                        {
                            return NotFound();
                        }
                    }
                }
            }

            return View(restaurant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RestaurantResponseModel reqObj)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("username")))
            {
                RestaurantResponseModel restaurant = new RestaurantResponseModel();
                var requestModel = new RestaurantRequestModel()
                {
                    Id = reqObj.Id,
                    Address = reqObj.Address,
                    Description = reqObj.Description,
                    Name = reqObj.Name,
                    PhoneNumber = reqObj.PhoneNumber,
                    WebsiteUrl = reqObj.WebsiteUrl,
                    UserId = HttpContext.Session.GetString("username")
                };
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsync($"https://localhost:7123/api/Restaurant/{reqObj.Id}", new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json")))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            restaurant = JsonConvert.DeserializeObject<RestaurantResponseModel>(apiResponse);
                            TempData["ResultOk"] = "Data Updated Successfully !";
                            return RedirectToAction("Index");
                        }
                    }
                }

            }

            return View(reqObj);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:7123/api/Restaurant/{id}"))
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
