using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace PIAWHTS.Processes
{
    public class DeletePersonal
    {
        public static string SqlConnect = "Data Source=DESKTOP-IHM6JP4\\SQLEXPRESS;Initial Catalog=PIAWHTS.DB;Integrated Security=True";
        public static void ErasePersonal()
        {
            Console.WriteLine("Xahiş olunur Personal nömrənizi daxil edin:");
            int PersonalNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Məlumatları Silindi:");
            ShowExistingPersonal.ShowPersonal(PersonalNumber);
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            sqlConnection.Open();
            string sqlQuery = "delete from [dbo].[tblPersonalInformation] where PersonalNumber = @PersonalNumber";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@PersonalNumber", PersonalNumber);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
