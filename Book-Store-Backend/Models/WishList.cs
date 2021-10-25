namespace Book_Store_Backend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class WishListChangeModel
    {
        public int BookId;
    }

    [Table("WishList")]
    public class WishList
    {
        public WishList()
        {
            Books = new HashSet<Book>();
        }
        [Key,ForeignKey("User")]
        public string WishListId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}