namespace Book_Store_Backend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;

    [Table("Book")]
    public partial class Book
    {
        public int BookId { get; set; }

        public int? CategoryId { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public long? ISBN { get; set; }

        public int? Year { get; set; }

        public double? Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public double? Position { get; set; }

        public bool? Status { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        public DateTime? createdAt { get; set; }

        public virtual Category Category { get; set; }


        List<Category> BookList = new List<Category>();
        SqlConnection con = new SqlConnection("server=IND433;database=BookStore; integrated security=true");
        SqlCommand cmd_createBook = new SqlCommand("Insert into Category values @BookId, @CategoryId, @Title, @ISBN, @Year, @Price,@Description,@Position,@Status,@Image @CreatedAt");
        SqlCommand cmd_listBook = new SqlCommand("Select * from Category");
        SqlCommand cmd_updateBook = new SqlCommand("update Category set  CategoryId=@newCategory, Title=@newTitle, ISBN=@newISBN, Year=@newYear, Price=@newPrice,Description=@newDescription,Position=@newPosition,Status=@newStatus,Image=@newImage CreatedAt=@newCreatedAt where BookId=@newID");
        SqlCommand cmd_deleteBook = new SqlCommand("Delete from Category values @BookId, @CategoryId, @Title, @ISBN, @Year, @Price,@Description,@Position,@Status,@Image @CreatedAt");

        public int AddAccount(int p_BookId, int p_CategoryId, string p_Title, long p_ISBN, int p_Year, double p_Price, string p_Description, double p_Position, bool p_Status, string p_Image, DateTime p_CreatedAt)
        {
            cmd_createBook.Connection = con;
            cmd_createBook.Parameters.AddWithValue("@BookId", p_BookId);
            cmd_createBook.Parameters.AddWithValue("@CategoryId", p_CategoryId);
            cmd_createBook.Parameters.AddWithValue("@Title", p_Title);
            cmd_createBook.Parameters.AddWithValue("@ISBN", p_ISBN);
            cmd_createBook.Parameters.AddWithValue("@Year", p_Year);
            cmd_createBook.Parameters.AddWithValue("@Price", p_Price);
            cmd_createBook.Parameters.AddWithValue("@Description", p_Description);
            cmd_createBook.Parameters.AddWithValue("@Position", p_Position);
            cmd_createBook.Parameters.AddWithValue("@Status", p_Status);
            cmd_createBook.Parameters.AddWithValue("@Image", p_Image);
            cmd_createBook.Parameters.AddWithValue("@CreatedAt", p_CreatedAt);

            con.Open();
            int result = cmd_createBook.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public List<Category> ListCategory()
        {
            cmd_listBook.Connection = con;
            SqlDataReader _read;


            con.Open();
            _read = cmd_listBook.ExecuteReader();
            while (_read.Read())
            {

                BookList.Add(new Category()
                {
                    CategoryId = Convert.ToInt32(_read[0]),
                    CategoryName = _read[1].ToString(),
                    Description = _read[2].ToString(),
                    Image = _read[3].ToString(),
                    Status = Convert.ToBoolean(_read[4]),
                    Position = Convert.ToDouble(_read[5]),
                    CreatedAt = Convert.ToDateTime(_read[6])

                });


            }
            _read.Close();
            con.Close();
            return BookList;

        }
        public int UpdateBook(int p_BookId, int p_CategoryId, string p_Title, long p_ISBN, int p_Year, double p_Price, string p_Description, double p_Position, bool p_Status, string p_Image, DateTime p_CreatedAt)
        {
            cmd_listBook.Connection = con;
            cmd_listBook.Parameters.AddWithValue("@BookId", p_BookId);
            cmd_listBook.Parameters.AddWithValue("@CategoryId", p_CategoryId);
            cmd_listBook.Parameters.AddWithValue("@Title", p_Title);
            cmd_listBook.Parameters.AddWithValue("@ISBN", p_ISBN);
            cmd_listBook.Parameters.AddWithValue("@Year", p_Year);
            cmd_listBook.Parameters.AddWithValue("@Price", p_Price);
            cmd_listBook.Parameters.AddWithValue("@Description", p_Description);
            cmd_listBook.Parameters.AddWithValue("@Position", p_Position);
            cmd_listBook.Parameters.AddWithValue("@Status", p_Status);
            cmd_listBook.Parameters.AddWithValue("@Image", p_Image);
            cmd_listBook.Parameters.AddWithValue("@CreatedAt", p_CreatedAt);
            con.Open();
            int result = cmd_updateBook.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteBook(int p_BookId)
        {
            cmd_deleteBook.Connection = con;
            cmd_deleteBook.Parameters.AddWithValue("BookId", p_BookId);
            con.Open();
            int result = cmd_deleteBook.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
