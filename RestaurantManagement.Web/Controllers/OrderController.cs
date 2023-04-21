using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;
using System.Text;

namespace RestaurantManagement.Web.Controllers
{
    public class OrderController : Controller
    {
        public async Task<IActionResult> PlaceOrder()
        {
            List<MenuResponseModel> cart = SessionHelper.GetObjectFromJson<List<MenuResponseModel>>(HttpContext.Session, "cart");

            using (var httpClient = new HttpClient())
            {
                var requestModel = new OrderRequestModel()
                {
                    OrderDateTime = DateTime.UtcNow,
                    OrderNumber = $"RM-{cart.FirstOrDefault().RestaurantId}",
                    TotalPrice = cart.Sum(item => item.Price * item.Quantity),
                    
                };

                foreach(var item in cart)
                {
                    requestModel.OrderDetails.Add(new OrderDetailRequestModel()
                    {
                        MenuId = item.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.Price
                    });
                }

                using (var response = await httpClient.PostAsync("https://localhost:7123/api/Order", new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json")))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["ResultOk"] = "Record Added Successfully !";
                        return RedirectToAction("Index", "Restaurant", new { area = "" });
                    }
                }
            }

            return View();
        }

        public async Task<IActionResult> Index()
        {
            var orderList = new PagedResults<OrderResponseModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7123/api/Order"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        orderList = JsonConvert.DeserializeObject<PagedResults<OrderResponseModel>>(apiResponse);
                    }
                }
            }
            return View(orderList);
        }
    }
}
