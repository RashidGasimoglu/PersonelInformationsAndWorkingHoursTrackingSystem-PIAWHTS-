using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace PIAWHTS.Processes
{
    public class UpdatePersonal
    {
        public static string SqlConnect = "Data Source=DESKTOP-IHM6JP4\\SQLEXPRESS;Initial Catalog=PIAWHTS.DB;Integrated Security=True";
        public static int PersonalNumber = 0;
        public static void UpdatePersonalInfo()
        {
            Console.WriteLine("Xahiş olunur Personal nömrənizi daxil edin:");
            PersonalNumber = Convert.ToInt32(Console.ReadLine());
            ShowExistingPersonal.ShowPersonal(PersonalNumber);
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            sqlConnection.Open();
            Console.WriteLine("Xahiş olunur Yeni Adı daxil edin:");
            string UpdatedName = Console.ReadLine().ToLower();
            Console.WriteLine("Xahiş olunur Yeni Soyadı daxil edin:");
            string UpdatedSurname = Console.ReadLine().ToLower();
            Console.WriteLine("Xahiş olunur Yeni Işə Daxil Olma Tarixi'ni daxil edin:");
            DateTime UpdatedDateOfEmployment = Convert.ToDateTime(Convert.ToDateTime(Console.ReadLine()).ToString("yyyy/MM/dd"));
            Console.WriteLine("Xahiş olunur Yeni Vəzifə'ni daxil edin:");
            string UpdatedPosition = Console.ReadLine().ToLower();
            Console.WriteLine("Xahiş olunur Yeni Əmək Haqqı Əmsalı'nı daxil edin:");
            float UpdatedRateOfWage = Convert.ToSingle(Console.ReadLine());
            string sqlQuery = $"update [dbo].[tblPersonalInformation] SET [Name] = '{UpdatedName}',[Surname] = '{UpdatedSurname}',[DateOfEmployment] = '{UpdatedDateOfEmployment}',[Position] = '{UpdatedPosition}',[RateOfWage] = {UpdatedRateOfWage} where [PersonalNumber] = {PersonalNumber}";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            ShowExistingPersonal.ShowPersonal(PersonalNumber);
        }
    }
}

