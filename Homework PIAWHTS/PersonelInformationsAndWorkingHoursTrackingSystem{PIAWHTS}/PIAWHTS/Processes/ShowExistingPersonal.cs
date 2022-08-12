using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace PIAWHTS.Processes
{
    public class ShowExistingPersonal
    {
        public static string SqlConnect = "Data Source=DESKTOP-IHM6JP4\\SQLEXPRESS;Initial Catalog=PIAWHTS.DB;Integrated Security=True";
        public static int PersonalNumber = 0;
        public static void ShowPersonal()
        {
            Console.WriteLine("Xahiş olunur Personal nömrənizi daxil edin:");
            PersonalNumber = Convert.ToInt32(Console.ReadLine());
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            SqlDataReader sqlDataReader;
            sqlConnection.Open();
            string sqlQuery = $"select personalinfo.PersonalNumber, personalinfo.Name, personalinfo.Surname, personalinfo.DateOfEmployment, personalinfo.Position, personalinfo.RateOfWage, personalinfo.MonthlyWorkingTime from [dbo].[tblPersonalInformation] as personalinfo where PersonalNumber = {PersonalNumber};";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Personal Nömrə | Ad Soyad | Işə giriş tarixi | Vəzifəsi | Əmək haqqı əmsalı");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            while (sqlDataReader.Read())
            {
                Console.WriteLine($"{sqlDataReader.GetValue(0)} | {sqlDataReader.GetValue(1)} {sqlDataReader.GetValue(2)} | {sqlDataReader.GetValue(3)} | {sqlDataReader.GetValue(4)} | {sqlDataReader.GetValue(5)}");
            }
        }
        public static void ShowPersonal(int PersonalNumber)
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            SqlDataReader sqlDataReader;
            sqlConnection.Open();
            string sqlQuery = $"select personalinfo.PersonalNumber, personalinfo.Name, personalinfo.Surname, personalinfo.DateOfEmployment, personalinfo.Position, personalinfo.RateOfWage, personalinfo.MonthlyWorkingTime from [dbo].[tblPersonalInformation] as personalinfo where PersonalNumber = {PersonalNumber};";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Personal Nömrə | Ad Soyad | Işə giriş tarixi | Vəzifəsi | Əmək haqqı əmsalı");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            while (sqlDataReader.Read())
            {
                Console.WriteLine($"{sqlDataReader.GetValue(0)} | {sqlDataReader.GetValue(1)} {sqlDataReader.GetValue(2)} | {sqlDataReader.GetValue(3)} | {sqlDataReader.GetValue(4)} | {sqlDataReader.GetValue(5)}");
            }
        }
    }
}
