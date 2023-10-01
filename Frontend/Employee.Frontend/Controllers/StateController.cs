﻿using Employee.Frontend.Models;
using Employee.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace Employee.Frontend.Controllers;

    public class StateController : Controller
    {

        private readonly HttpClient _httpClient;
    //public StateController(IHttpClientFactory httpClient) 
    //{
    //    _httpClient = httpClient.CreateClient("EmployeeAPIBase");

    //}

    public StateController(IHttpClientFactory httpClient)=> _httpClient = httpClient.CreateClient("EmployeeAPIBase");

    

    //private async Task<IEnumerable<State>> GetAllState()
    //{
    //    var responce = await _httpClient.GetAsync("State");
    //    if (responce.IsSuccessStatusCode)

    //    {
    //        var content = await responce.Content.ReadAsStringAsync();
    //        var states = JsonConvert.DeserializeObject<List<State>>(content);
    //        return states!;
    //    }
    //    return new List<State>();
    //}

    public async Task<List<State>> GetAllState()
    {
        var response = await _httpClient.GetFromJsonAsync<List<State>>("State");
        return response is not null ? response : new List<State>();
    }



    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var data = await GetAllState();
        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> AddOrEdit(int Id)
    {
        if (Id == 0)
        {
            return View(new State());
        }
        else
        {
            var data = await _httpClient.GetAsync($"State/{Id}");
            if (data.IsSuccessStatusCode)
            {
                var result = await data.Content.ReadFromJsonAsync<State>();
                return View(result);
            }


        }
        return View(new State());
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> AddOrEdit(int Id, State State)
    {
        if (ModelState.IsValid)
        {
            if (Id == 0)
            {
                var result = await _httpClient.PostAsJsonAsync("State", State);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var result = await _httpClient.PutAsJsonAsync($"State/{Id}", State);
                if (result.IsSuccessStatusCode) { return RedirectToAction("Index"); }
            }


        }
        return View(new State());
    }
    [HttpGet]

    public async Task<IActionResult> Delete(int Id)
    {
        var data = await _httpClient.DeleteAsync($"State/{Id}");
        if (data.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else return NotFound();
    }
}


