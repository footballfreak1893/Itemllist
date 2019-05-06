using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ItemList.Classes
{
    class UserSettings
    {
        string pathUserSettings = @"Data/userSettings.txt";
        //Array muss erweitert werden, wenn neue Userwerte kommen !!!
        private string[] arrUserSettings = new string[3];

        public void SetBackgroundColor(string userBackground, bool LoadUserSettings)
        {
            
            Console.WriteLine("Set Background-color");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Black [0]");
            Console.WriteLine("Red [1]");
            Console.WriteLine("Yellow [2]");
            Console.WriteLine("Green [3]");
            Console.WriteLine("Blue [4]");
            Console.WriteLine("Magenta [5]");
            Console.WriteLine("Gray [6]");
            Console.WriteLine("Whilte [7]");

            if (LoadUserSettings == false)
            {
                userBackground = Console.ReadLine();
            }

            Console.WriteLine("Hallo "+userBackground);
            var backgroundNumber = CheckingNumbers.CheckingValuesINT(userBackground);
           

            switch (backgroundNumber)
            {
                case 0:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;

                case 1:
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;

                case 2:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;

                case 3:
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;

                case 4:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;

                case 5:
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;

                case 6:
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;

                case 7:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;

                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    backgroundNumber = 0;
                    break;

            }
            arrUserSettings[0] = backgroundNumber.ToString();
            SaveUserSettings(arrUserSettings);
            Console.Clear();
        }


        public void SetFontColor(string userFont, bool LoadUserSettings)
        {
            Console.WriteLine("Set Font-color");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Black [0]");
            Console.WriteLine("Red [1]");
            Console.WriteLine("Yellow [2]");
            Console.WriteLine("Green [3]");
            Console.WriteLine("Blue [4]");
            Console.WriteLine("Magenta [5]");
            Console.WriteLine("Gray [6]");
            Console.WriteLine("Whilte [7]");

            if (LoadUserSettings == false)
            {
                userFont = Console.ReadLine();
            }
            var fontNumber = CheckingNumbers.CheckingValuesINT(userFont);

            switch (fontNumber)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;

                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case 2:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case 4:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;

                case 5:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;

                case 6:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;

                case 7:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    fontNumber = 7;
                    break;
            }
            arrUserSettings[1] = fontNumber.ToString();
            Console.WriteLine(arrUserSettings[0]);
            SaveUserSettings(arrUserSettings);
            Console.Clear();
        }

        public void SettingsMenu()
        {
            Console.WriteLine("Settings Menu");
            Console.WriteLine();

            while(true)
            {
                Console.WriteLine("Change font color [fc]");
                Console.WriteLine("Change background color [bc]");
                Console.WriteLine("Open Main menu, if the programm start [m]");
                Console.WriteLine("Reset the programm [r]");

                var userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "fc":
                        SetFontColor(arrUserSettings[1], false);
                        //SaveUserSettings();
                        break;

                    case "bc":
                        SetBackgroundColor(arrUserSettings[0], false);
                        //SaveUserSettings();
                        break;

                    case "m":
                        Console.Clear();
                        StartView(arrUserSettings);
                        Console.Clear();
                        break;

                    case "r":
                        ResetProgramm();
                        break;

                    default:
                        //SaveUserSettings();
                        return;
                }

            }
        }

        public void SaveUserSettings(string [] arrUserSettings)
        {
            File.WriteAllLines(pathUserSettings, arrUserSettings);
        }

        public void LoadUserSettings()
        {
            if (!File.Exists(pathUserSettings) )
            {
                File.WriteAllText(pathUserSettings, "");
                SetBackgroundColor("0", true);
                SetFontColor("7", true);
                arrUserSettings[2] = "0";
                SaveUserSettings(arrUserSettings);
            }
            arrUserSettings = File.ReadAllLines(pathUserSettings);

            SetBackgroundColor(arrUserSettings[0], true);
            SetFontColor(arrUserSettings[1], true);

            if (arrUserSettings[2] == "1")
            {
                List list = new List();
                Data data = new Data();
                list.OpenDefaultList(data);
            }
        }

        public void StartView(string [] arrUserSettings)
        {
            if (arrUserSettings[2] == "0")
            {
                Console.WriteLine("Open the defaultList, if programm starts [1]");
            }
            if (arrUserSettings[2] == "1")
            {
                Console.WriteLine("Open the main menu, if programm starts[0]");
            }
            var userinput = Console.ReadLine();

            if (userinput == "1")
            {
                arrUserSettings[2] = "1";
                SaveUserSettings(arrUserSettings);
            }

            else
            {
                arrUserSettings[2] = "0";
                SaveUserSettings(arrUserSettings);
            }
        }

        public void ResetProgramm()
        {
            Data data = new Data();
            Directory.Delete("Data", true);
            Environment.Exit(0);
     
            
        }
    }
}
