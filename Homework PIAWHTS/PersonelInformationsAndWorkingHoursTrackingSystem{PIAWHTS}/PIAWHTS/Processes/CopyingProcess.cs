using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace PIAWHTS.Processes
{
    public class CopyingProcess
    {
        public static string SqlConnect = "Data Source=DESKTOP-IHM6JP4\\SQLEXPRESS;Initial Catalog=PIAWHTS.DB;Integrated Security=True";
        public static void Copying()
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            sqlConnection.Open();
            string sqlQuery = "DROP TABLE [dbo].[tblPersonalInformationBackUp];";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            CopyingProcess.CreatingBackupTable();
            CopyingProcess.PullFromOriginalToBackup();
            Console.WriteLine("Personalların Aylıq Məlumatları Nüsxələndi!");
        }
        public static void CreatingBackupTable()
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            sqlConnection.Open();
            string sqlQuery = "CREATE TABLE [dbo].[tblPersonalInformationBackUp]([PersonalNumber] [int] NOT NULL,[Name] [nvarchar](25) NOT NULL,[Surname] [nvarchar](25) NOT NULL,[DateOfEmployment] [date] NOT NULL,[Position] [nvarchar](15) NOT NULL,[RateOfWage] [float] NOT NULL,[MonthlyWorkingTime] [int] NOT NULL,[LateForWorkCount] [int] NOT NULL);";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public static void PullFromOriginalToBackup()
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            SqlDataReader sqlDataReader;
            sqlConnection.Open();
            string sqlQuery = $"select personalinfo.PersonalNumber, personalinfo.Name, personalinfo.Surname, personalinfo.DateOfEmployment, personalinfo.Position, personalinfo.RateOfWage, personalinfo.MonthlyWorkingTime, personalinfo.LateForWorkCount from [dbo].[tblPersonalInformation] as personalinfo;";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                int PersonalNumber = (int)sqlDataReader.GetValue(0);
                string Name = (string)sqlDataReader.GetValue(1);
                string Surname = (string)sqlDataReader.GetValue(2);
                DateTime DateOfEmployment = (DateTime)sqlDataReader.GetValue(3);
                string Position = (string)sqlDataReader.GetValue(4);
                float RateOfWage = (float)Convert.ToSingle(sqlDataReader.GetValue(5));
                int MonthlyWorkingTime = (int)sqlDataReader.GetValue(6);
                int LateForWorkCount = (int)sqlDataReader.GetValue(7);
                CopyingProcess.InsertFromOriginalToBackup(PersonalNumber, Name, Surname, DateOfEmployment, Position, RateOfWage, MonthlyWorkingTime, LateForWorkCount);
            }
            sqlConnection.Close();
        }
        public static void InsertFromOriginalToBackup(int PersonalNumber, string Name, string Surname, DateTime DateOfEmployment, string Position, float RateOfWage, int MonthlyWorkingTime, int LateForWorkCount)
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            sqlConnection.Open();
            string sqlQuery = $"insert into dbo.tblPersonalInformationBackUp(PersonalNumber,Name,Surname,DateOfEmployment,Position,RateOfWage,MonthlyWorkingTime,LateForWorkCount) values(@PersonalNumber,@Name,@Surname,@DateOfEmployment,@Position,@RateOfWage,@MonthlyWorkingTime,@LateForWorkCount);";
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
        }
    }
}
