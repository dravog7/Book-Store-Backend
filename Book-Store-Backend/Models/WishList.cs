namespace Book_Store_Backend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.ComponentModel.DataAnnotations.Schema;

    public class WishList
    {
        public int id { get; set; }

        [ForeignKey("user")]
        public int userId { get; set; }
        public ApplicationUser user { get; set; }
        public string title { get; set; }
        public List<Book> books;
    }
}