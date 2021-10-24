namespace Book_Store_Backend.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
        public int? CouponId;
    }

    public class Order
    {
        public Order()
        {
            BookEntries = new HashSet<BookEntry>();
            Coupons = new HashSet<Coupon>();
        }
        public int OrderId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }
        public virtual ICollection<BookEntry> BookEntries { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public OrderStatus status { get; set; }
        [StringLength(2000)]
        public string address { get; set; }
        public float price { get; set; }
    }

    public class BookEntry
    {
        [Key, Column(Order = 0)]
        public int BookId { get; set; }
        [Key, Column(Order = 1)]
        public int OrderId { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
        public virtual Book Book { get; set; }
        [DefaultValue(1)]
        public int quantity { get; set; } = 1;
    }
}