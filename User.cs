using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace YourNote
{  
    public class User
    {
        
        public string Userid;
        public string Password;
        public string Name;

        public static bool IsValidEmail(string email)
        {
            Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
            return emailRegex.IsMatch(email);
        }

        public User(string userid, string password, string name)
        {
            Userid = userid;
            Password = password;
            Name = name;
        }

        
         public  void NewUser()
        {
            Console.WriteLine("Enter the Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the UserID:");
            string id = Console.ReadLine();
            while (IsValidEmail(id) == false) 
            {

                Console.WriteLine("Please Provide Valid EmailId");
                Console.WriteLine("Enter the UserID:");
                 id = Console.ReadLine();
            }
            Console.WriteLine("Enter the Password:");
            string password = Program.getData();
            string pass = (string)password;
            User newuser = new User(id, pass, name);

           
          DBUpdation.InsertNewUser(newuser);
        }



        public void UserLogin(string userId)
        {
            UserMenuOptions(userId);
        }

        public static void ViewAllNotes(string userId)
        {
            
            do 
            {
                Note note = new Note("", "", "");
                Console.WriteLine("\t\t\tChoose From The Below\t\t\t");
                Console.WriteLine("\t\t\t1.To View Personal Notes\t\t\t");
                Console.WriteLine("\t\t\t2.To View Shared Notes\t\t\t");
                Console.WriteLine("\t\t\t3.Go Back To UserMenu Options\t\t\t");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        {

                            note.PrintAllNotes(userId);
                            Console.WriteLine("\n");
                            break;
                        }

                    case "2":
                        {
                            note.ViewSharedNotes(userId);
                            Console.WriteLine("\n");
                            break;

                        }
                    case "3":
                        {
                            return;

                            
                        }
                    default:
                        {
                            Console.WriteLine("\t\t\tPlease choose from the options\t\t\t");
                            break;
                        }
                }



            } while (true);

        }
         public static void UserMenuOptions(string userId)
        {
            string choice;
            do
            {
                Note note = new Note("", "", "");

                Console.WriteLine("\t\t\tChoose From The UserMenu Options\t\t\t");
                Console.WriteLine("\t\t\t1.Create New Note\t\t\t");
                Console.WriteLine("\t\t\t2.View All Notes\t\t\t");
                Console.WriteLine("\t\t\t3.View A Note\t\t\t");
                Console.WriteLine("\t\t\t4.Share a Note\t\t\t");
                Console.WriteLine("\t\t\t5.Exit\t\t\t");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            
                            Note.CreateNote(userId);
                            Console.WriteLine("\n");
                            break;
                        }

                    case "2":
                        {
                            ViewAllNotes(userId);
                            Console.WriteLine("\n");
                            break;
                       
                        }
                    case "3":
                        {  
                            note.ViewNote(userId);
                            Console.WriteLine("\n");
                            break;
                        }

                    case "4":
                        {
                            note.ShareNotes(userId);
                            Console.WriteLine("\n");
                            break;
                        }
                    case "5":
                        {
                            return;
                        }
                    default:
                        {;
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

    }

  
}
