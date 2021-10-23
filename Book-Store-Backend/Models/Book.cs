namespace Book_Store_Backend.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;

    [Table("Book")]
    public partial class Book
    {
        public Book()
        {
            WishLists = new HashSet<WishList>();
        }
        public int BookId { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(50)]
        public string ISBN { get; set; }

        public int? Year { get; set; }

        public double? Price { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        [DefaultValue(0)]
        public double Position { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;

        [StringLength(200)]
        public string Image { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime createdAt { get; set; }


        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
