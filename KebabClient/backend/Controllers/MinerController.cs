using Kebab.Managers;
using KebabClient.Managers;
using KebabClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace KebabClient.Controllers;
public class MinerController(MinerManager minerManager): Controller
{
    [HttpGet]
    public async Task<IActionResult> Chain()
    { 
        var chain = await minerManager.GetChain(); 
        return Ok(chain);
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}