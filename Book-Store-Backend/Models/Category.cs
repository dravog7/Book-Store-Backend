namespace Book_Store_Backend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;

    [Table("Category")]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public int CategoryId { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        public bool? Status { get; set; }

        public double? Position { get; set; }

        public DateTime? CreatedAt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Books { get; set; }

        List<Category> CatList = new List<Category>();
        SqlConnection con = new SqlConnection("server=IND433;database=BookStore; integrated security=true");
        SqlCommand cmd_createCategory = new SqlCommand("Insert into Category values @CategoryId, @CategoryName, @Description, @Image, @Status, @Position, @CreatedAt");
        SqlCommand cmd_listCategory = new SqlCommand("Select * from Category");
        SqlCommand cmd_updateCategory = new SqlCommand("update Category set  CategoryName=@newName Description=@newDescription Image=@newStatus Position=@newPosition CreatedAt=@newCreatedAt where CategoryId=@newID");
        SqlCommand cmd_deleteCategory = new SqlCommand("Delete from Category values @CategoryId, @CategoryName, @Description, @Image, @Status, @Position, @CreatedAt");

        public int AddAccount(int p_CategoryId, string p_CategoryName, string p_Description, string p_Image, bool p_Status, double p_Position, DateTime p_CreatedAt)
        {
            cmd_createCategory.Connection = con;
            cmd_createCategory.Parameters.AddWithValue("@CategoryId", p_CategoryId);
            cmd_createCategory.Parameters.AddWithValue("@CategoryName", p_CategoryName);
            cmd_createCategory.Parameters.AddWithValue("@Description", p_Description);
            cmd_createCategory.Parameters.AddWithValue("@Image", p_Image);
            cmd_createCategory.Parameters.AddWithValue("@Status", p_Status);
            cmd_createCategory.Parameters.AddWithValue("@Position", p_Position);
            cmd_createCategory.Parameters.AddWithValue("@CreatedAt", p_CreatedAt);

            con.Open();
            int result = cmd_createCategory.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public List<Category> ListCategory()
        {
            cmd_listCategory.Connection = con;
            SqlDataReader _read;


            con.Open();
            _read = cmd_listCategory.ExecuteReader();
            while (_read.Read())
            {

                CatList.Add(new Category()
                {
                    CategoryId = Convert.ToInt32(_read[0]),
                    CategoryName = _read[1].ToString(),
                    Description = _read[2].ToString(),
                    Image = _read[3].ToString(),
                    Status = Convert.ToBoolean(_read[4]),
                    Position=Convert.ToDouble(_read[5]),
                    CreatedAt=Convert.ToDateTime(_read[6])

                }) ;


            }
            _read.Close();
            con.Close();
            return CatList;

        }
        public int UpdateCategory(string p_CategoryName, string p_Description, string p_Image, bool p_Status, double p_Position, DateTime p_CreatedAt, int p_CategoryId)
        {
            cmd_updateCategory.Connection = con;
            cmd_updateCategory.Parameters.AddWithValue("@CategoryName", p_CategoryName);
            cmd_updateCategory.Parameters.AddWithValue("@Description", p_Description);
            cmd_updateCategory.Parameters.AddWithValue("@Image", p_Image);
            cmd_updateCategory.Parameters.AddWithValue("@Status", p_Status);
            cmd_updateCategory.Parameters.AddWithValue("@Position", p_Position);
            cmd_updateCategory.Parameters.AddWithValue("@CreatedAt", p_CreatedAt);
            cmd_updateCategory.Parameters.AddWithValue("@newNo", p_CategoryId);
            con.Open();
            int result = cmd_updateCategory.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteCategory(int p_CategoryId)
        {
            cmd_deleteCategory.Connection = con;
            cmd_deleteCategory.Parameters.AddWithValue("CategoryId", p_CategoryId);
            con.Open();
            int result = cmd_deleteCategory.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
