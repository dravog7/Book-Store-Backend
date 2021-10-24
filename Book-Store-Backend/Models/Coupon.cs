namespace Book_Store_Backend.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    [Table("Coupon")]
    public class Coupon
    {
        public Coupon()
        {
            Orders = new HashSet<Order>();
        }
        public int Id { get; set; }
        [StringLength(500)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Code { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}