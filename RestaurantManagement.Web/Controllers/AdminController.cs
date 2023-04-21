using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantManagement.Domain.DTO.Response;

namespace RestaurantManagement.Web.Controllers
{
    public class AdminController : Controller
    {
        public async Task<IActionResult> Login(string returnUrl)
        {
            var model = new AdminResponseModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminResponseModel model)
        {
            var responseModel = new AdminResponseModel();
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"https://localhost:7123/api/Admin?username={model.UserName}&password={model.Password}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            responseModel = JsonConvert.DeserializeObject<AdminResponseModel>(apiResponse);
                            HttpContext.Session.SetString("username", responseModel.UserName);
                            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                            {
                                return Redirect(model.ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Restaurant");
                            }
                        }
                       
                    }
                }

              
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Restaurant");
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }
    }
}
