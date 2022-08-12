using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PIAWHTS.Processes
{
    public class ShowDailyWorktimeOfPersonals
    {
        public static string SqlConnect = "Data Source=DESKTOP-IHM6JP4\\SQLEXPRESS;Initial Catalog=PIAWHTS.DB;Integrated Security=True";
        public static int DayOfMonth = 0;
        public static int DailyWorktimeHour = 0;
        public static int DailyWorktimeMinute = 0;
        public static void ShowDailyPersonalWorktime()
        {
            Console.WriteLine("Xahiş olunur hansı Günün iş cədvəlini görmək istəyirsinizsə qeyd edin:");
            DayOfMonth = Convert.ToInt32(Console.ReadLine());
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            SqlDataReader sqlDataReader;
            sqlConnection.Open();
            string sqlQuery = $"SELECT tblMonthlyWorktimeInfo.PersonalNumber, [DayOfMonth], [EntryHour], [EntryMinute], [ExitHour], [ExitMinute], tblPersonalInformation.Name, tblPersonalInformation.Surname FROM [dbo].[tblMonthlyWorktimeInfo] inner join tblPersonalInformation on tblPersonalInformation.PersonalNumber = tblMonthlyWorktimeInfo.PersonalNumber where tblMonthlyWorktimeInfo.DayOfMonth = {DayOfMonth};";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Personal Nömrə | Ad Soyad | Işə giriş | Işdən Çıxış | Günlük Iş Vaxtı");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            while (sqlDataReader.Read())
            {
                DailyWorktimeHour = (int)sqlDataReader.GetValue(4)-(int)sqlDataReader.GetValue(2);
                if ((int)sqlDataReader.GetValue(5) >= (int)sqlDataReader.GetValue(3))
                {
                    DailyWorktimeMinute = (int)sqlDataReader.GetValue(5) - (int)sqlDataReader.GetValue(3);
                }
                if ((int)sqlDataReader.GetValue(5) < (int)sqlDataReader.GetValue(3))
                {
                    DailyWorktimeMinute = 60 + ((int)sqlDataReader.GetValue(5) - (int)sqlDataReader.GetValue(3));
                    DailyWorktimeHour--;
                }
                Console.WriteLine($"{sqlDataReader.GetValue(0)} | {sqlDataReader.GetValue(6)} {sqlDataReader.GetValue(7)} | {sqlDataReader.GetValue(2)}:{sqlDataReader.GetValue(3)} | {sqlDataReader.GetValue(4)}:{sqlDataReader.GetValue(5)} | {DailyWorktimeHour} saat {DailyWorktimeMinute} dəqiqə ");
            }
        }
    }
}
