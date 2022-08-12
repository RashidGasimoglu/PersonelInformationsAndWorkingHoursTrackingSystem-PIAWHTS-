using PIAWHTS.Processes;
using System;
using System.Threading;

namespace PIAWHTS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Menu();
        }
        public static void Menu()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Işçilərin kadr məlumatlarının və iş vaxtının izlənməsi proqramı.");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------"); 
            Thread.Sleep(50);
            Console.WriteLine("Menyudan istifadə etmək üçün xahiş olunur, seçmək istədiyiniz bölməyə uyğun gələn sıra nömrəsindən istifadə edəsiniz!");
            Thread.Sleep(50);
            bool returnbacktostart = true;
            do
            {
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("1. Məlumatların Göstərilməsi.");
                Thread.Sleep(50);
                Console.WriteLine("2. Məlumatların Əlavəsi,Yenilənməsi və yaxud Silinməsi.");
                Thread.Sleep(50);
                Console.WriteLine("3. Məlumatların Nüsxələnməsi.");
                string mainmenu = Console.ReadLine();
                switch (mainmenu)
                {
                    case "1":
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("1. Işçinin məlumatlarının göstərilməsi.");
                        Thread.Sleep(50);
                        Console.WriteLine("2. Işçinin aylıq iş müddəti, alacağı əmək haqqı və iş vaxtlarının göstərilməsi.");
                        Thread.Sleep(50);
                        Console.WriteLine("3. Işçinin/Işçilərin vəzifəyə görə tapılması və sıralanması.");
                        Thread.Sleep(50);
                        Console.WriteLine("4. Işə qəbul olunan işçilərin sayının illər üzrə bölgüsünün göstərilməsi.");
                        Thread.Sleep(50);
                        Console.WriteLine("5. Işə gec gələn işçilərin siyahısının göstərilməsi.");
                        Thread.Sleep(50);
                        Console.WriteLine("6. Istənilən gün üçün işə gələn bütün işçilərin iş saatlarının göstərilməsi.");
                        string firstmenu = Console.ReadLine();
                        switch (firstmenu)
                        {
                            case "1":
                                ShowExistingPersonal.ShowPersonal();
                                break;
                            case "2":
                                ShowPersonalWageAndWorkTime.ShowPersonalInfoAndWage();
                                break;
                            case "3":
                                ShowPersonalsForTheirPositions.FindPersonalInfoFromPosition();
                                break;
                            case "4":
                                ShowPersonalsForTheirEntryYear.ShowPersonalsForEntryYear();
                                break;
                            case "5":
                                ShowLateForWorkPersonals.ShowLateForWorkPersonal();
                                break;
                            case "6":
                                ShowDailyWorktimeOfPersonals.ShowDailyPersonalWorktime();
                                break;
                        }
                        break;
                    case "2":
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("1. Yeni işçinin əlavə olunması.");
                        Thread.Sleep(50);
                        Console.WriteLine("2. Işçi məlumatlarının yenilənməsi.");
                        Thread.Sleep(50);
                        Console.WriteLine("3. Istənilən gün üçün işçilərin iş vaxtlarının əlavə olunması.");
                        Thread.Sleep(50);
                        Console.WriteLine("4. Işçi məlumatlarının silinməsi.");
                        string secondmenu = Console.ReadLine();
                        switch (secondmenu)
                        {
                            case "1":
                                AddPersonal.CreateNewPersonal();
                                break;
                            case "2":
                                UpdatePersonal.UpdatePersonalInfo();
                                break;
                            case "3":
                                AddWorktime.AddDailyWorktime();
                                break;
                            case "4":
                                DeletePersonal.ErasePersonal();
                                break;
                        }
                        break;
                    case "3":
                        CopyingProcess.Copying();
                        break;
                }
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Menyu'ya qayıtmaq istəyirsinizmi ?");
                Thread.Sleep(50);
                Console.WriteLine("1. Menyu'ya qayıtmaq.");
                Thread.Sleep(50);
                Console.WriteLine("2. Çıxış.");
                string returnmainmenu = Console.ReadLine();
                if (returnmainmenu != "1")
                {
                    returnbacktostart = false;
                }
                Console.Clear();
            } while (returnbacktostart == true);
        }
    }
}