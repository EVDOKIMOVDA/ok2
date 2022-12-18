using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaDelivery.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.ViewModels
{
    public class PizzaVM
    {
        public Pizza Pizza { get; set; }
        public IEnumerable<SelectListItem> TDDRestaurant { get; set; }
    }
}
