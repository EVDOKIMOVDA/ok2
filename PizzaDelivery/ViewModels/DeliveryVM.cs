using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaDelivery.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.ViewModels
{
    public class DeliveryVM
    {
        public Delivery Delivery { get; set; }
        public IEnumerable<SelectListItem> TDDClient { get; set; }
        public IEnumerable<SelectListItem> TDDCourier { get; set; }
        public IEnumerable<SelectListItem> TDDDelivery_type { get; set; }
        public IEnumerable<SelectListItem> TDDPizza { get; set; }
    }
}
