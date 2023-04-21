using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantManagement.Domain.DTO.Response;
using System.Diagnostics;

namespace RestaurantManagement.Web.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<MenuResponseModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Price * item.Quantity);
            var restaurantId = cart.FirstOrDefault()?.RestaurantId;
            ViewBag.RestaurantId = restaurantId;
            if (restaurantId == null)
                return RedirectToAction("Index", "Restaurant");
            return View();
        }
        public async Task<IActionResult> Buy(int id)
        {
            var dbMenu = new MenuResponseModel();
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync($"https://localhost:7123/api/Menu/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        dbMenu = JsonConvert.DeserializeObject<MenuResponseModel>(apiResponse);
                    }
                }
            }

            MenuResponseModel menuResponseModel = new MenuResponseModel();
            if (SessionHelper.GetObjectFromJson<List<MenuResponseModel>>(HttpContext.Session, "cart") == null)
            {
                List<MenuResponseModel> cart = new List<MenuResponseModel>();
                cart.Add(new MenuResponseModel { Id = dbMenu.Id, CategoryId = dbMenu.CategoryId,Description = dbMenu.Description,Name = dbMenu.Name,Price = dbMenu.Price,RestaurantId = dbMenu.RestaurantId, Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<MenuResponseModel> cart = SessionHelper.GetObjectFromJson<List<MenuResponseModel>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new MenuResponseModel { Id = dbMenu.Id, CategoryId = dbMenu.CategoryId, Description = dbMenu.Description, Name = dbMenu.Name, Price = dbMenu.Price, RestaurantId = dbMenu.RestaurantId, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<MenuResponseModel> cart = SessionHelper.GetObjectFromJson<List<MenuResponseModel>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<MenuResponseModel> cart = SessionHelper.GetObjectFromJson<List<MenuResponseModel>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
