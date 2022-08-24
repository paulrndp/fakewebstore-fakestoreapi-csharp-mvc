using FakeWebStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;

namespace FakeWebStore.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://fakestoreapi.com/products");
        HttpClient client;

        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            List<Product> modelList = new List<Product>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<Product>>(data);
            }
            return View(modelList);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}