using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaDelivery.Data;
using PizzaDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Controllers
{
    public class PizzaController : Controller
    {
        private readonly DataDbContext _db;

        public PizzaController(DataDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Pizza> objList = _db.Pizzas;

            foreach (var obj in objList)
            {
                obj.Restaurant = _db.Restaurants.FirstOrDefault(u => u.Id == obj.Restaurant_id);
            }
            return View(objList);
        }

        //GET-CREATE
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            PizzaVM pizzaVM = new PizzaVM()
            {
                Pizza = new Pizza(),
                TDDRestaurant = _db.Clients.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            return View(pizzaVM);
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Create(PizzaVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Pizzas.Add(obj.Pizza);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET-DELETE
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Pizzas.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST-DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Pizzas.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Pizzas.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET-UPDATE
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Update(int? id)
        {
            PizzaVM pizzaVM = new PizzaVM()
            {
                Pizza = new Pizza(),
                TDDRestaurant = _db.Clients.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            pizzaVM.Pizza = _db.Pizzas.Find(id);
            if (pizzaVM.Pizza == null)
            {
                return NotFound();
            }
            return View(pizzaVM);
        }

        //POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Update(PizzaVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Pizzas.Update(obj.Pizza);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _db.Pizzas.Include(b => b.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);

        }
    }
}
