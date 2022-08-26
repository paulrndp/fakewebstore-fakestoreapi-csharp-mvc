using FakeWebStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;

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
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product model = new Product();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<Product>(data);
            }
            return View(model);
        }
        [HttpPatch]
        public IActionResult Edit(Product obj)
        {
            string data = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/" + obj.id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
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