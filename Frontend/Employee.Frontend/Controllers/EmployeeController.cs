﻿using Employee.Frontend.Models;
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
    }

