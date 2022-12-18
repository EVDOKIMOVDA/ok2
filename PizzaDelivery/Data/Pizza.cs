using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Data
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Pizza_name { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int Price { get; set; }

        [ForeignKey("Restaurant_id")]
        public Restaurant Restaurant { get; set; }
        [DisplayName("Restaurant")]
        public int Restaurant_id { get; set; }

    }
}
