using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PIAWHTS.Processes
{
    public class ShowPersonalWageAndWorkTime
    {
        public static string SqlConnect = "Data Source=DESKTOP-IHM6JP4\\SQLEXPRESS;Initial Catalog=PIAWHTS.DB;Integrated Security=True";
        public static int PersonalNumber = 0;
        public static double Wage = 0;
        public static int TotalHour = 0;
        public static int TotalMinute = 0;
        public static void ShowPersonalInfoAndWage()
        {
            Console.WriteLine("Xahiş olunur Personal nömrənizi daxil edin:");
            PersonalNumber = Convert.ToInt32(Console.ReadLine());
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            SqlDataReader sqlDataReader;
            sqlConnection.Open();
            string sqlQuery = $"select personalinfo.PersonalNumber, personalinfo.Name, personalinfo.Surname, personalinfo.RateOfWage, personalinfo.MonthlyWorkingTime from [dbo].[tblPersonalInformation] as personalinfo where PersonalNumber = {PersonalNumber};";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Personal Nömrə | Ad Soyad | Aylıq Işləmə Müddəti | Maaşı ");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            while (sqlDataReader.Read())
            {
                Wage = (Convert.ToDouble(sqlDataReader.GetValue(4)) / 60) * 10 * Convert.ToDouble(sqlDataReader.GetValue(3));
                TotalHour = Convert.ToInt32(sqlDataReader.GetValue(4)) / 60;
                TotalMinute = Convert.ToInt32(sqlDataReader.GetValue(4)) - (TotalHour * 60);
                Console.WriteLine($"{sqlDataReader.GetValue(0)} | {sqlDataReader.GetValue(1)} {sqlDataReader.GetValue(2)} | {TotalHour} saat {TotalMinute} dəqiqə | {Wage}");

            }
            ShowPersonalWageAndWorkTime.ShowWorkdatesOfPersonal();
        }
        public static void ShowWorkdatesOfPersonal()
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            SqlDataReader sqlDataReader;
            sqlConnection.Open();
            string sqlQuery = $"select worktimeinfo.PersonalNumber, worktimeinfo.DayOfMonth, worktimeinfo.EntryHour, worktimeinfo.EntryMinute ,worktimeinfo.ExitHour, worktimeinfo.ExitMinute from [dbo].[tblMonthlyWorktimeInfo] as worktimeinfo where PersonalNumber = {PersonalNumber} order by worktimeinfo.DayOfMonth ASC;";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Işləmə Müddəti Rekordları:");
            Console.WriteLine("Gün | Işə giriş vaxtı | Işdən çıxış vaxtı");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            while (sqlDataReader.Read())
            {
                Console.WriteLine($"{sqlDataReader.GetValue(1)} | {sqlDataReader.GetValue(2)}:{sqlDataReader.GetValue(3)} | {sqlDataReader.GetValue(4)}:{sqlDataReader.GetValue(5)} |");
            }
        }
    }
}
