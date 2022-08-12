using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PIAWHTS.Processes
{
    public class ShowLateForWorkPersonals
    {
        public static string SqlConnect = "Data Source=DESKTOP-IHM6JP4\\SQLEXPRESS;Initial Catalog=PIAWHTS.DB;Integrated Security=True";
        public static int PersonalNumber = 0;
        public static void ShowLateForWorkPersonal()
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            SqlDataReader sqlDataReader;
            sqlConnection.Open();
            string sqlQuery = $"select personalinfo.PersonalNumber, personalinfo.Name, personalinfo.Surname, personalinfo.LateForWorkCount from [dbo].[tblPersonalInformation] as personalinfo where LateForWorkCount > 0;";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Personal Nömrə | Ad Soyad | Gec giriş etmə sayı");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            while (sqlDataReader.Read())
            {
                Console.WriteLine($"{sqlDataReader.GetValue(0)} | {sqlDataReader.GetValue(1)} {sqlDataReader.GetValue(2)} | {sqlDataReader.GetValue(3)}");
            }
        }
    }
}
