using System;
using System.IO;
using WSShell = IWshRuntimeLibrary.WshShell;
using WSShortcut = IWshRuntimeLibrary.IWshShortcut;

namespace ItemList.Classes
{
    class UserSettings
    {
        string pathUserSettings = @"Data/userSettings.txt";

        public static string pathStartUp = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        public static string shortcut = @"ItemList.lnk";
        public string shortcutPath = Path.Combine(pathStartUp, shortcut);
      
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
                userBackground = FontColorEqualsBackgroundColor(userBackground, 1);
            }

            var backgroundNumber = CheckingValues.CheckingValuesINT(userBackground);


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
                userFont = FontColorEqualsBackgroundColor(userFont, 0);
            }
            var fontNumber = CheckingValues.CheckingValuesINT(userFont);
            

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

        public string FontColorEqualsBackgroundColor(string inputcolor, int arrItem)
        {
            arrUserSettings = File.ReadAllLines(pathUserSettings);
            while (arrUserSettings[arrItem] == inputcolor)
            {
                Console.WriteLine("The background color can't equals the font color, try again");
                inputcolor = Console.ReadLine();
            }
            return inputcolor;
        }

        public string GetBackgroundColor()
        {
            arrUserSettings = File.ReadAllLines(pathUserSettings);
            return arrUserSettings[0];
        }

        public string GetFontColor()
        {
            arrUserSettings = File.ReadAllLines(pathUserSettings);
            return arrUserSettings[1];
        }

        public void SettingsMenu()
        {
            Console.WriteLine("Settings Menu");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Change font color [fc]");
                Console.WriteLine("Change background color [bc]");
                Console.WriteLine("Reset color [rc]");
                Console.WriteLine("Set Main menu, if the programm start [m]");

                if (!File.Exists(shortcutPath))
                {
                    Console.WriteLine("Add programm to autostart [a] ");
                }
                else
                {
                    Console.WriteLine("Remove programm from autostart [a] ");
                }
                Console.WriteLine("Reset the programm [r]");


                var userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "fc":
                        Console.Clear();
                        SetFontColor(arrUserSettings[1], false);
                        break;

                    case "bc":
                        Console.Clear();
                        SetBackgroundColor(arrUserSettings[0], false);
                        break;

                    case "rc":
                        Console.Clear();
                        SetBackgroundColor("0", true);
                        SetFontColor("7", true);
                        break;

                    case "m":
                        Console.Clear();
                        StartView(arrUserSettings);
                        Console.Clear();
                        break;

                    case "r":
                        ResetProgramm();
                        break;

                    case "a":
                        Console.Clear();
                        if (!File.Exists(shortcutPath))
                        {
                            AddToAutostart();
                        }
                        else
                        {
                            RemoveFromAutostart(shortcutPath);
                        }

                        break;

                    default:
                        return;
                }

            }
        }

        public void SaveUserSettings(string[] arrUserSettings)
        {
            File.WriteAllLines(pathUserSettings, arrUserSettings);
        }

        public void LoadUserSettings()
        {
            if (!File.Exists(pathUserSettings))
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
                ManageLists list = new ManageLists();
                ManageListItems data = new ManageListItems();
                list.OpenDefaultList(data);
            }
        }

        public void StartView(string[] arrUserSettings)
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
            ManageListItems data = new ManageListItems();
            Directory.Delete("Data", true);
            Environment.Exit(0);
        }

        public void AddToAutostart()
        {
            var shortcut = @"ItemList.lnk";
            var exe = "ItemList.exe";
            var pathExe=Path.Combine( Environment.CurrentDirectory, exe);
            
            //string shortcutPath = Path.Combine(pathStartUp, shortcut);

            WSShell wsho = new WSShell();
            WSShortcut sc = (WSShortcut)wsho.CreateShortcut(shortcutPath);
            sc.TargetPath = pathExe;
            sc.Description = "";
            sc.WorkingDirectory = Path.GetDirectoryName(pathExe);
            sc.Save();
        }

        public void RemoveFromAutostart(string shortcutPath)
        {
            File.Delete(shortcutPath);
        }
    }
}
    

