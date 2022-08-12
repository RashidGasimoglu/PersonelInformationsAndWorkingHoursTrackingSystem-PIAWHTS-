using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PIAWHTS.Processes
{
    public class ShowPersonalsForTheirEntryYear
    {
        public static string SqlConnect = "Data Source=DESKTOP-IHM6JP4\\SQLEXPRESS;Initial Catalog=PIAWHTS.DB;Integrated Security=True";
        public static int TotalPersonal = 0;
        public static void ShowPersonalsForEntryYear()
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            SqlDataReader sqlDataReader;
            sqlConnection.Open();
            string sqlQuery = "SELECT datepart(yyyy,DateOfEmployment), COUNT(datepart(yyyy,DateOfEmployment)) FROM [dbo].[tblPersonalInformation] GROUP BY datepart(yyyy,DateOfEmployment) HAVING COUNT(datepart(yyyy,DateOfEmployment)) > 0;";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Daxil olunan illər | Personal Sayı | Faizi ");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            ShowPersonalsForTheirEntryYear.CalculatePersonelNumber();
            while (sqlDataReader.Read())
            {
                Console.WriteLine($"{sqlDataReader.GetValue(0)} | {sqlDataReader.GetValue(1)} | {(100 / TotalPersonal) * (int)sqlDataReader.GetValue(1)}%");
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"Toplam Personal Sayı {TotalPersonal}");
        }
        public static void CalculatePersonelNumber()
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            SqlDataReader sqlDataReader;
            sqlConnection.Open();
            string sqlQuery = "SELECT COUNT(*) FROM [dbo].[tblPersonalInformation];";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                TotalPersonal = (int)sqlDataReader.GetValue(0);
            }
        }
    }
}
