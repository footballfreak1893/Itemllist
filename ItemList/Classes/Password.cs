using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemList.Classes
{
     class Password
    {
        //--> Hier könnte man noch Passwortrichtlinien erstellen
        //Bug: wenn passwort eingegen wird, das passwort kopiert ist, wird es sichtbar!!!!!

        public void SetPassword(Data data)
        {
            Console.WriteLine("Enter password");
            Console.ForegroundColor = ConsoleColor.Black;
            var password = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Password set");

            password = AES.CryptMenu(password, 'e');
            data.SaveString(password, data.pathPassword);
        }

        public void EnterPassword(Data data)
        {
            var password = data.ReadString(data.pathPassword);
            password = AES.CryptMenu(password, 'd');

            if (password == null || password == "")
            {
                return;
            }

            Console.WriteLine("This list requires a password, please enter it");
            Console.ForegroundColor = ConsoleColor.Black;
            var inputPassword = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            while (password != inputPassword)
            {
                Console.WriteLine("Password is incorrect, try again");
                Console.ForegroundColor = ConsoleColor.Black;
                inputPassword = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        public void Change_Or_SetNull_Password(Data data, char deleteOrChange)
        {
            if (deleteOrChange == 'd')
            {
                var password = "";
                data.SaveString(password, data.pathPassword);
            }

            else
            {
                Console.WriteLine("Change password");
                Console.WriteLine("enter current password");
                Console.ForegroundColor = ConsoleColor.Black;
                var inputPasswordCurrent = Console.ReadLine();
                var password = data.ReadString(data.pathPassword);
                password = AES.CryptMenu(password, 'd');
                Console.ForegroundColor = ConsoleColor.White;

                while (password != inputPasswordCurrent)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Current password is incorrect, try again");
                    Console.ForegroundColor = ConsoleColor.Black;
                    inputPasswordCurrent = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("enter new password");
                Console.ForegroundColor = ConsoleColor.Black;
                password = Console.ReadLine();
                password = AES.CryptMenu(password, 'e');
                data.SaveString(password, data.pathPassword);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Password has successfully changed");
            }
        }

        public void PasswortSettings(Data data)
        {
            //--> BUG: Wenn Feature Farbe ändern eingebaut wird muss andere Lösung her als vordergrundfarbe zu ändern (Idee -->..)
            Console.WriteLine("Password settings");
            Console.WriteLine();

            var password = data.ReadString(data.pathPassword);
            password = AES.CryptMenu(password, 'd');

            if (password == null || password == "")
            {
                Console.WriteLine("add password [a]");
            }

            Console.WriteLine("Change password [c]");
            Console.WriteLine("Deactivate password [d]");
            var userinput = Console.ReadLine();
            userinput = userinput.ToLower();

            switch (userinput)
            {
                case "a":
                    SetPassword(data);
                    break;

                case "c":
                    Change_Or_SetNull_Password(data, 'c');
                    break;

                case "d":
                    Change_Or_SetNull_Password(data, 'd');
                    break;

                case "p":

                    break;

                default:
                    return;
            }
            Console.Clear();
        }
    }
}
