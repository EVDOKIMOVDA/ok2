using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Data
{
    public class Delivery
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Order_date { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        public DateTime Delivery_date { get; set; }
        [Required]
        public int Delivery_price { get; set; }


        [ForeignKey("Client_id")]
        public Client Client{ get; set; }
        [DisplayName("Client")]
        public int Client_id { get; set; }


        [ForeignKey("Courier_id")]
        public Courier Courier { get; set; }
        [DisplayName("Courier")]
        public int Courier_id { get; set; }


        [ForeignKey("Pizza_id")]
        public Pizza Pizza { get; set; }
        [DisplayName("Pizza")]
        public int Pizza_id { get; set; }
    }
}
