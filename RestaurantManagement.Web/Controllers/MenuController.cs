using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;
using System.Text;

namespace RestaurantManagement.Web.Controllers
{
    public class MenuController : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            List<MenuResponseModel> menuList = new List<MenuResponseModel>();
            using (var httpClient = new HttpClient())
            {
                
                using (var response = await httpClient.GetAsync($"https://localhost:7123/api/Menu/restaurant/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        menuList = JsonConvert.DeserializeObject<List<MenuResponseModel>>(apiResponse);
                    }
                }
            }
            ViewBag.RestaurantId = id;
            return View(menuList);
        }

        public async Task<IActionResult> Create(int id)
        {
            var categoryList = new PagedResults<CategoryResponseModel>();
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync($"https://localhost:7123/api/Category"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        categoryList = JsonConvert.DeserializeObject<PagedResults<CategoryResponseModel>>(apiResponse);
                    }
                }
            }


            ViewBag.Categories = categoryList.Results
                     .Select(i => new SelectListItem
                     {
                         Value = i.Id.ToString(),
                         Text = i.Name
                     }).ToList();


            ViewBag.RestaurantId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuResponseModel reqModel)
        {
            //var ss = Request.Form["RestaurantId"];
            MenuResponseModel menu = new MenuResponseModel();
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("username")))
            {
                using (var httpClient = new HttpClient())
                {
                    var requestModel = new MenuRequestModel()
                    {
                        Description = reqModel.Description,
                        CategoryId = reqModel.CategoryId,
                        Price = reqModel.Price,
                        Quantity = reqModel.Quantity,
                        RestaurantId = reqModel.RestaurantId,
                        Name = reqModel.Name,
                        UserId = HttpContext.Session.GetString("username")
                    };

                    using (var response = await httpClient.PostAsync("https://localhost:7123/api/Menu", new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json")))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            TempData["ResultOk"] = "Record Added Successfully !";
                            return RedirectToAction("Index", new { id = requestModel.RestaurantId });
                        }
                    }
                }

            }

            return View(menu);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            MenuResponseModel menu = new MenuResponseModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7123/api/Menu/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        menu = JsonConvert.DeserializeObject<MenuResponseModel>(apiResponse);
                        if (menu == null)
                        {
                            return NotFound();
                        }
                        ViewBag.RestaurantId = menu.RestaurantId;
                    }
                }
            }
            var categoryList = new PagedResults<CategoryResponseModel>();
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync($"https://localhost:7123/api/Category"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        categoryList = JsonConvert.DeserializeObject<PagedResults<CategoryResponseModel>>(apiResponse);
                    }
                }
            }


            ViewBag.Categories = categoryList.Results
                     .Select(i => new SelectListItem
                     {
                         Value = i.Id.ToString(),
                         Text = i.Name
                     }).ToList();
            return View(menu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MenuResponseModel reqObj)
        {
            MenuResponseModel menu = new MenuResponseModel();
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("username")))
            {

                var requestModel = new MenuRequestModel()
                {
                    Id = reqObj.Id,
                    Description = reqObj.Description,
                    Name = reqObj.Name,
                    UserId = HttpContext.Session.GetString("username"),
                    CategoryId = reqObj.CategoryId,
                    Price = reqObj.Price,
                    Quantity = reqObj.Quantity,
                    RestaurantId = reqObj.RestaurantId
                };
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsync($"https://localhost:7123/api/Menu/{reqObj.Id}", new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json")))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            TempData["ResultOk"] = "Data Updated Successfully !";
                            return RedirectToAction("Index", new { id = requestModel.RestaurantId });
                        }
                    }
                }

            }

            return View(menu);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:7123/api/Menu/{id}"))
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
