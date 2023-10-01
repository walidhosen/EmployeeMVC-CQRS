using Employee.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace Employee.Frontend.Controllers;

public class EmployeeController : Controller
{
    private readonly HttpClient _httpClient;

    //public EmployeeController(IHttpClientFactory httpClient)
    //{ 
    //    _httpClient = httpClient.CreateClient("EmployeeAPIBase");
    //}


    public EmployeeController(IHttpClientFactory httpClient) => _httpClient = httpClient.CreateClient("EmployeeAPIBase");


    //private async Task<IEnumerable<Employees>> GetAllEmployee()
    //{
    //    var responce = await _httpClient.GetAsync("Employee");
    //    if (responce.IsSuccessStatusCode)
    //    {
    //        var content = await responce.Content.ReadAsStringAsync();
    //        var employees = JsonConvert.DeserializeObject<IEnumerable<Employees>>(content);
    //        return employees!;
    //    }
    //    return new List<Employees>();
    //}

    public async Task<List<Employees>> GetAllEmployee()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Employees>>("Employee");
        return response is not null ? response : new List<Employees>();
    }




    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var data = await GetAllEmployee();
        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> AddOrEdit(int Id)
    {
        if (Id == 0)
        {
            return View(new Employees());
        }
        else
        {
            var data = await _httpClient.GetAsync($"Empolyee/{Id}");
            if (data.IsSuccessStatusCode)
            {
                var result = await data.Content.ReadFromJsonAsync<Employees>();
                return View(result);
            }


        }
        return View(new Employees());
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> AddOrEdit(int Id, Employees emp)
    {
        if (ModelState.IsValid)
        {
            if (Id == 0)
            {
                var result = await _httpClient.PostAsJsonAsync("Employee", emp);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var result = await _httpClient.PutAsJsonAsync($"Empolyee/{Id}", emp);
                if (result.IsSuccessStatusCode) { return RedirectToAction("Index"); }
            }


        }
        return View(new Employees());
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int Id)
    {
        var data = await _httpClient.DeleteAsync($"Employee?Id={Id}");
        if (data.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else return NotFound();
    }
}
