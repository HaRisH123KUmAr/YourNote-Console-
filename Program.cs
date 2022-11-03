using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace YourNote
{
   public class Program
    {
        User user = new User("", "", "");

        public static string getData()
        {
            string sub = "", ans = "";
            char pick;
            do
            {
                pick = Console.ReadKey(true).KeyChar;
                sub += pick;
            } while (pick != (char)ConsoleKey.Enter);

            for (int i = 0; i < sub.Length-1; i++)
                ans += sub[i];
          return ans ;
        }

        void MenuOptions()
        {
            
            string choice;
            do
            {
                Console.WriteLine("\t\t\tChoose From The MainMenu Options\t\t\t");
                Console.WriteLine("\t\t\t1.New User\t\t\t");
                Console.WriteLine("\t\t\t2.Login\t\t\t");
                Console.WriteLine("\t\t\t3.List All the Users\t\t\t");
                Console.WriteLine("\t\t\t4.Exit\t\t\t");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            UserCreation();
                            Console.WriteLine("\n");
                            break;
                        }

                    case "2":
                        {
                            UserLogin();
                            Console.WriteLine("\n");
                            break;
                        }
                    case "3":
                        {
                            PrintAllUsers();
                            Console.WriteLine("\n");
                            break;
                        }
                    case "4":
                        {
                            return; 
                        }
                    default:
                        {

                            Console.WriteLine("\t\t\tPlease choose from the options\t\t\t");
                            break;
                        }

                }
                //Console.WriteLine("\t\t\tDo you wish to continue, then Press : Y/y \t\t\t");

                //choice = Console.ReadLine();

            }
            //       while (choice=="Y"|| choice=="y")   ;
            while (true);
        }

        void UserCreation()
        {
            Console.WriteLine("\t\t\tCreation Of New User : \t\t\t");
            user.NewUser();
        }
        public void PrintAllUsers()
        {
            if (DBFetch.DataExist(DBCreation.userTableName) == false)
            { 
                Console.WriteLine("\t\t\tNo Users Exist \t\t\t\n");
                return;
            }

            Console.WriteLine("\t\t\tThe User Details are : \t\t\t\n");
            Console.WriteLine("UserID | Name");
            DBFetch.ReadAllData(DBCreation.userTableName);
           
           

        }

        void UserLogin()
        {   
           
            Console.WriteLine("Enter the UserID:");
            string userId = Console.ReadLine();
            Console.WriteLine("Enter the Password:");
            string pass = getData();

            DBFetch dBFetch = new DBFetch();
            string name = dBFetch.CheckUser(DBCreation.userTableName, userId + pass);
            if (name != "")
            {
                Console.WriteLine($"\t\t\t\tWelcome, {name}!\t\t\t");
                Console.WriteLine("\t\t\t---------------------------------\t\t");
                user.UserLogin(userId);

            }
            else
            {
                Console.WriteLine("\t\t\tPlease check the Credentials!\t\t\t");
            }



            return;
        }
        static void Main(string[] args)
        {
            Program pg = new Program();
            Console.WriteLine("\t\t\tWelcome To YourNote\t\t\t");
            DBCreation.UserTableCreation();
            DBCreation.NotesTableCreation();
            DBCreation.SharedNotesTableCreation();
            
            pg.MenuOptions();
            Console.WriteLine("\t\t\tThank You!\t\t\t");
        }

    }
    
}
