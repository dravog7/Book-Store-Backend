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

    public class OrderCouponModel
    {
        public int? CouponId;
    }

    public class OrderChangeModel
    {
        public OrderStatus? status;
        public string address;
        public int? couponId;
    }

    public class Order
    {
        public Order()
        {
            BookEntries = new HashSet<BookEntry>();
        }
        public int OrderId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<BookEntry> BookEntries { get; set; }

        [ForeignKey("Coupon")]
        public int? CouponId { get; set; }
        public virtual Coupon Coupon { get; set; }
        public OrderStatus status { get; set; } = OrderStatus.IN_PROGRESS;
        [StringLength(2000)]
        public string address { get; set; }
        public double price { get; set; }

        public void setTotalPrice()
        {
            double totalPrice = 0;
            foreach(var bookEntry in this.BookEntries)
            {
                totalPrice += bookEntry.quantity * bookEntry.Book.Price??0;
            }
            totalPrice -= Math.Min(totalPrice * Coupon.Discount / 100, Coupon.MaxDiscount);
            this.price = totalPrice;
        }
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