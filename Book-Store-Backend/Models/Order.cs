namespace Book_Store_Backend.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;
    
    public enum OrderStatus
    {
        IN_PROGRESS,
        PLACED,
        CANCELLED
    }

    public class OrderChangeModel
    {
        public int? BookId;
        public int? CouponId;
    }

    public class Order
    {
        public Order()
        {
            Books = new HashSet<Book>();
            Coupons = new HashSet<Coupon>();
        }
        public int OrderId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public OrderStatus status { get; set; }
        [StringLength(2000)]
        public string address { get; set; }
        public float price { get; set; }
    }
}