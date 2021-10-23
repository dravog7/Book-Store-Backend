namespace Book_Store_Backend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    [Table("WishList")]
    public class WishList
    {
        public WishList()
        {
            Books = new HashSet<Book>();
        }
        public int WishListId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}