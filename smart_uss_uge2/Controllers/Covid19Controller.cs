using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using smart_uss_uge2.Models;
using smart_uss_uge2.Services;

namespace smart_uss_uge2.Controllers
{
    public class Covid19Controller : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;
        public Covid19Controller(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [Authorize]
        [ActionName("Soeg")]
        public IActionResult Soeg()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ActionName("Soeg")]
        public async Task<IActionResult> Soeg([Bind("Cpr,Telefonnummer")]SoegViewModel search)
        {
     /*   var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();*/
            if (ModelState.IsValid)
            {
                String sql = "SELECT * FROM c WHERE c.cpr = '" + search.Cpr + "'";
                return View("Index", await _cosmosDbService.GetItemsAsync(sql));
                
                /*List<Covid19> items = (List<Covid19>)await _cosmosDbService.GetItemsAsync(sql);
                if (items.Count == 0)
                {
                    return NotFound();
                }
                return View("Edit",items[0]);*/
            }
            return View();
        }

        [Authorize]
        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c"));
        }

        [Authorize]
        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Name,Cpr,Adresse,By,Postnummer,Telefonnummer,TestTaget, ErPositiv")] Covid19 item)
        {

            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddItemAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [Authorize]
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id,Name,Cpr,Adresse,By,Postnummer,Telefonnummer,TestTaget,ErPositiv")] Covid19 item)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.UpdateItemAsync(item.Id, item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [Authorize]
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Covid19 item = await _cosmosDbService.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [Authorize]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Covid19 item = await _cosmosDbService.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _cosmosDbService.GetItemAsync(id));
        }
    }
}
