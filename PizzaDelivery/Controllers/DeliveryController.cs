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
    public class DeliveryController : Controller
    {
        private readonly DataDbContext _db;

        public DeliveryController(DataDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Delivery> objList = _db.Deliveries;

            foreach (var obj in objList)
            {
                obj.Client = _db.Clients.FirstOrDefault(u => u.Id == obj.Client_id);
                obj.Courier = _db.Couriers.FirstOrDefault(u => u.Id == obj.Client_id);
                obj.Pizza = _db.Pizzas.FirstOrDefault(u => u.Id == obj.Client_id);
            }
            return View(objList);
        }

        //GET-CREATE
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            DeliveryVM deliveryVM = new DeliveryVM()
            {
                Delivery = new Delivery(),
                TDDClient = _db.Clients.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                TDDCourier = _db.Couriers.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                TDDPizza = _db.Pizzas.Select(i => new SelectListItem
                {
                    Text = i.Pizza_name,
                    Value = i.Id.ToString()
                }),
            };

            return View(deliveryVM);
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Create(DeliveryVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Deliveries.Add(obj.Delivery);
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
            var obj = _db.Deliveries.Find(id);
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
            var obj = _db.Deliveries.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Deliveries.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET-UPDATE
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Update(int? id)
        {
            DeliveryVM deliveryVM = new DeliveryVM()
            {
                Delivery = new Delivery(),
                TDDClient = _db.Clients.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                TDDCourier = _db.Couriers.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                TDDPizza = _db.Pizzas.Select(i => new SelectListItem
                {
                    Text = i.Pizza_name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            deliveryVM.Delivery = _db.Deliveries.Find(id);
            if (deliveryVM.Delivery == null)
            {
                return NotFound();
            }
            return View(deliveryVM);
        }

        //POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Update(DeliveryVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Deliveries.Update(obj.Delivery);
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

            var delivery = await _db.Deliveries.Include(b => b.Client).Include(c => c.Courier).Include(d => d.Pizza)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);

        }
    }
}
