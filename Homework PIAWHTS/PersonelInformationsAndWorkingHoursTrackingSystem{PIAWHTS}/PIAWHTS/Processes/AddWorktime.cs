using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace PIAWHTS.Processes
{
    public class AddWorktime
    {
        public static string SqlConnect = "Data Source=DESKTOP-IHM6JP4\\SQLEXPRESS;Initial Catalog=PIAWHTS.DB;Integrated Security=True";
        public static int DayOfMonth = 0;
        public static int PersonalNumber = 0;
        public static void AddDailyWorktime()
        {
            Console.WriteLine("Xahiş olunur Personal nömrənizi daxil edin:");
            PersonalNumber = Convert.ToInt32(Console.ReadLine());
            ShowExistingPersonal.ShowPersonal(PersonalNumber);
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            SqlDataReader sqlDataReader;
            sqlConnection.Open();
            string sqlQuery = $"select personalinfo.MonthlyWorkingTime,personalinfo.LateForWorkCount from [dbo].[tblPersonalInformation] as personalinfo where [PersonalNumber] = {PersonalNumber};";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            int MonthlyWorkingTime = 0;
            int LateForWorkCount = 0;
            while (sqlDataReader.Read())
            {
                MonthlyWorkingTime = (int)sqlDataReader.GetValue(0);
                LateForWorkCount = (int)sqlDataReader.GetValue(1);
            }
            sqlConnection.Close();
            AddWorktime.ProcessWorktime(MonthlyWorkingTime, LateForWorkCount, PersonalNumber);
            AddWorktime.ShowPersonalsDailyWorktime(PersonalNumber, DayOfMonth);
        }
        public static void ProcessWorktime(int MonthlyWorkingTime,int LateForWorkCount,int PersonalNumber)
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            sqlConnection.Open();
            Console.WriteLine("Ayın hansı günü üçün qeyd aparmaq istəyirsinizsə, həmin günü qeyd edin:");
            DayOfMonth = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Giriş Vaxtını qeyd edin (Saat):");
            int EntryHour = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Giriş Vaxtını qeyd edin (Dəqiqə):");
            int EntryMinute = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Çıxış Vaxtını qeyd edin (Saat):");
            int ExitHour = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Çıxış Vaxtını qeyd edin (Dəqiqə):");
            int ExitMinute = Convert.ToInt32(Console.ReadLine());
            int UpdatedMonthlyWorkingTime = 0;
            if (EntryHour >= 9 && EntryMinute > 0 && ExitHour < 17)
            {
                LateForWorkCount++;
                UpdatedMonthlyWorkingTime = (((ExitHour * 60) + ExitMinute) - ((EntryHour * 60) + EntryMinute)) + MonthlyWorkingTime;
            }
            else if (EntryHour < 9 && ExitHour > 16)
            {
                int EnteredInTime = 9;
                int ExitedInTime = 17;
                UpdatedMonthlyWorkingTime = ((ExitedInTime * 60) - (EnteredInTime * 60)) + MonthlyWorkingTime;
            }
            else if (EntryHour < 9)
            {
                int EnteredInTime = 9;
                UpdatedMonthlyWorkingTime = (((ExitHour * 60) + ExitMinute) - (EnteredInTime * 60)) + MonthlyWorkingTime;
            }
            else if (ExitHour > 16)
            {
                LateForWorkCount++;
                int ExitedInTime = 17;
                UpdatedMonthlyWorkingTime = ((ExitedInTime * 60) - ((EntryHour * 60) + EntryMinute)) + MonthlyWorkingTime;
            }
            string sqlQuery = $"insert into dbo.tblMonthlyWorktimeInfo(PersonalNumber,DayOfMonth,EntryHour,EntryMinute,ExitHour,ExitMinute) values(@PersonalNumber,@DayOfMonth,@EntryHour,@EntryMinute,@ExitHour,@ExitMinute); update dbo.tblPersonalInformation SET [MonthlyWorkingTime] = {UpdatedMonthlyWorkingTime},[LateForWorkCount] = {LateForWorkCount}  where [PersonalNumber] = {PersonalNumber};";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@PersonalNumber", PersonalNumber);
            sqlCommand.Parameters.AddWithValue("@DayOfMonth", DayOfMonth);
            sqlCommand.Parameters.AddWithValue("@EntryHour", EntryHour);
            sqlCommand.Parameters.AddWithValue("@EntryMinute", EntryMinute);
            sqlCommand.Parameters.AddWithValue("@ExitHour", ExitHour);
            sqlCommand.Parameters.AddWithValue("@ExitMinute", ExitMinute);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public static void ShowPersonalsDailyWorktime(int PersonalNumber, int DayOfMonth)
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            SqlDataReader sqlDataReader;
            sqlConnection.Open();
            string sqlQuery = $"select worktimeinfo.PersonalNumber, worktimeinfo.DayOfMonth, worktimeinfo.EntryHour, worktimeinfo.EntryMinute ,worktimeinfo.ExitHour, worktimeinfo.ExitMinute from [dbo].[tblMonthlyWorktimeInfo] as worktimeinfo where PersonalNumber = {PersonalNumber} and [DayOfMonth] = {DayOfMonth};";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Personal Nömrə | Iş günü | Işə giriş vaxtı | Işdən çıxış vaxtı ");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            while (sqlDataReader.Read())
            {
                Console.WriteLine($"{sqlDataReader.GetValue(0)} | {sqlDataReader.GetValue(1)} | {sqlDataReader.GetValue(2)}:{sqlDataReader.GetValue(3)} | {sqlDataReader.GetValue(4)}:{sqlDataReader.GetValue(5)}");
            }
        }
    }
}