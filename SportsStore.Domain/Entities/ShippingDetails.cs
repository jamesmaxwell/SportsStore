using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please entry a name...")]
        public string Name { get; set; }

        [Required()]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required()]
        public string City { get; set; }

        [Required()]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required()]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
