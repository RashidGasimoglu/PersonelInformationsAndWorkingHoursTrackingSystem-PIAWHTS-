using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace PIAWHTS.Processes
{
    public class AddPersonal
    {
        public static string SqlConnect = "Data Source=DESKTOP-IHM6JP4\\SQLEXPRESS;Initial Catalog=PIAWHTS.DB;Integrated Security=True";
        public static int PersonalNumber = 0;
        public static void CreateNewPersonal()
        {
            Random random = new Random();
            PersonalNumber = random.Next(1, 100);
            Console.WriteLine("Adınızı daxil edin:");
            string Name = Console.ReadLine().ToLower();
            Console.WriteLine("Soyadınızı daxil edin:");
            string Surname = Console.ReadLine().ToLower();
            Console.WriteLine("Işə başlama tarixini daxil edin (il.ay.gun [iiii.aa.gg] formatında):");
            DateTime DateOfEmployment = Convert.ToDateTime(Convert.ToDateTime(Console.ReadLine()).ToString("yyyy/MM/dd"));
            Console.WriteLine("Vəzifənizi daxil edin:");
            string Position = Console.ReadLine().ToLower();
            Console.WriteLine("Əmək haqqı əmsalını daxil edin:");
            float RateOfWage = Convert.ToSingle(Console.ReadLine());
            int MonthlyWorkingTime = 0;
            int LateForWorkCount = 0;
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            sqlConnection.Open();
            string sqlQuery = "insert into dbo.tblPersonalInformation(PersonalNumber,Name,Surname,DateOfEmployment,Position,RateOfWage,MonthlyWorkingTime,LateForWorkCount) values(@PersonalNumber,@Name,@Surname,@DateOfEmployment,@Position,@RateOfWage,@MonthlyWorkingTime,@LateForWorkCount);";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@PersonalNumber", PersonalNumber);
            sqlCommand.Parameters.AddWithValue("@Name", Name);
            sqlCommand.Parameters.AddWithValue("@Surname", Surname);
            sqlCommand.Parameters.AddWithValue("@DateOfEmployment", DateOfEmployment.ToShortDateString());
            sqlCommand.Parameters.AddWithValue("@Position", Position);
            sqlCommand.Parameters.AddWithValue("@RateOfWage", RateOfWage);
            sqlCommand.Parameters.AddWithValue("@MonthlyWorkingTime", MonthlyWorkingTime);
            sqlCommand.Parameters.AddWithValue("@LateForWorkCount", LateForWorkCount);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close(); 
            Console.Clear();
            AddPersonal.ShowNewPersonel();
        }
        public static void ShowNewPersonel()
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            SqlDataReader sqlDataReader;
            sqlConnection.Open();
            string sqlQuery = $"select personalinfo.PersonalNumber, personalinfo.Name, personalinfo.Surname, personalinfo.DateOfEmployment, personalinfo.Position, personalinfo.RateOfWage from [dbo].[tblPersonalInformation] as personalinfo where PersonalNumber = {PersonalNumber};";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Qeydiyyatdan Uğurla Keçdiniz.");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Thread.Sleep(50);
                Console.WriteLine($"Şəxsi Nömrəniz: {sqlDataReader.GetValue(0)}");
                Thread.Sleep(50);
                Console.WriteLine($"Adınız: {sqlDataReader.GetValue(1)}");
                Thread.Sleep(50);
                Console.WriteLine($"Soyadınız: {sqlDataReader.GetValue(2)}");
                Thread.Sleep(50);
                Console.WriteLine($"Işə Başlama Tarixiniz: {sqlDataReader.GetValue(3)}");
                Thread.Sleep(50);
                Console.WriteLine($"Vəzifəniz: {sqlDataReader.GetValue(4)}");
                Thread.Sleep(50);
                double RateOfWageWithDigits = (double)sqlDataReader.GetValue(5);
                Console.WriteLine($"Əmək Haqqı Əmsalınız: {RateOfWageWithDigits.ToString("0.00")}");
                Thread.Sleep(50);
            }
        }
    }
}
