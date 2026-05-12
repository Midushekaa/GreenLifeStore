using System;
using System.Data;
using System.Data.SqlClient;
using GreenLifeStore.DataLayer;

namespace GreenLifeStore.Sub_class
{
    public class Discount
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Apply discount (insert into DB)
        public void ApplyDiscount(DatabaseHelper db)
        {
            string query = @"
                INSERT INTO Discount (Product_Id, Discount_Percentage, Start_Date, End_Date, Created_At, Updated_At)
                VALUES (@ProductId, @DiscountPercentage, @StartDate, @EndDate, GETDATE(), GETDATE())";

            db.ExecuteNonQuery(query, new SqlParameter[]
            {
                new SqlParameter("@ProductId", ProductId),
                new SqlParameter("@DiscountPercentage", DiscountPercentage),
                new SqlParameter("@StartDate", StartDate),
                new SqlParameter("@EndDate", EndDate)
            });
        }
        // Update discount
        public void UpdateDiscount(DatabaseHelper db)
        {
            string query = @"
                UPDATE Discount
                SET Product_Id=@ProductId, Discount_Percentage=@DiscountPercentage,
                    Start_Date=@StartDate, End_Date=@EndDate, Updated_At=GETDATE()
                WHERE Id=@Id";

            db.ExecuteNonQuery(query, new SqlParameter[]
            {
                new SqlParameter("@ProductId", ProductId),
                new SqlParameter("@DiscountPercentage", DiscountPercentage),
                new SqlParameter("@StartDate", StartDate),
                new SqlParameter("@EndDate", EndDate),
                new SqlParameter("@Id", Id)
            });
        }
        // Load all discounts
        public static DataTable LoadDiscounts(DatabaseHelper db)
        {
            string query = @"
                SELECT d.Id, p.Name AS Product, d.Discount_Percentage, d.Start_Date, d.End_Date
                FROM Discount d
                JOIN Product p ON d.Product_Id = p.Id";

            return db.ExecuteQuery(query);
        }


        // Delete discount by Id
        public static void DeleteDiscount(int discountId, DatabaseHelper db)
        {
            string query = "DELETE FROM Discount WHERE Id=@Id";
            db.ExecuteNonQuery(query, new SqlParameter[]
            {
                new SqlParameter("@Id", discountId)
            });
        }
    }
}