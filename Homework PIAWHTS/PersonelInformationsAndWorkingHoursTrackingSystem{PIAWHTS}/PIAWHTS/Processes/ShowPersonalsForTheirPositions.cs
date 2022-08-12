using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace PIAWHTS.Processes
{
    public class ShowPersonalsForTheirPositions
    {
        public static string SqlConnect = "Data Source=DESKTOP-IHM6JP4\\SQLEXPRESS;Initial Catalog=PIAWHTS.DB;Integrated Security=True";
        public static void FindPersonalInfoFromPosition()
        {
            Console.WriteLine("Personalın/Personalların Vəzifə'sini daxil edin:");
            string PersonalPosition = Console.ReadLine();
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            SqlDataReader sqlDataReader;
            sqlConnection.Open();
            string sqlQuery = "select personalinfo.PersonalNumber, personalinfo.Name, personalinfo.Surname, personalinfo.DateOfEmployment, personalinfo.Position, personalinfo.RateOfWage, personalinfo.MonthlyWorkingTime from [dbo].[tblPersonalInformation] as personalinfo order by personalinfo.RateOfWage DESC;";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Personal Nömrə | Ad Soyad | Işə giriş tarixi | Əmək haqqı Əmsalı");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            while (sqlDataReader.Read())
            {
                if (PersonalPosition == Convert.ToString(sqlDataReader.GetValue(4)))
                {
                    Console.WriteLine($"{sqlDataReader.GetValue(0)} | {sqlDataReader.GetValue(1)} {sqlDataReader.GetValue(2)} | {sqlDataReader.GetValue(3)} | {sqlDataReader.GetValue(5)}");
                }
            }
        }
    }
}
