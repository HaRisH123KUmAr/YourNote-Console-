using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YourNote
{
    public class DBFetch
    {
        //Checks If there are notes created by the user 
        public static bool DataExist(string tableName, string userId)
        {
            bool value = false;
            try
            {
                long noOfRows = 0;
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                string columncount = $"SELECT COUNT(*) FROM '{tableName}' where '{tableName}'.UserId='{userId}';";
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = columncount;
                noOfRows = (Int64)sqlite_cmd.ExecuteScalar();
                conn.Close();

                if (noOfRows != 0)
                    value = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return value;
        }

        // Checks if there is general data in the table 
        public static bool DataExist(string tableName)
        {
            bool value = false;
            try
            { long noOfRows = 0;
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                string columncount = $"SELECT COUNT(*) FROM ('{tableName}');";
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = columncount;
                noOfRows = (Int64)sqlite_cmd.ExecuteScalar();
                conn.Close();

                if(noOfRows!=0)
                    value = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return value;
        }

        // Checks if the user's id and password is present in the table or not 
        public string CheckUser(string tablename, string credentials)
        {

            try 
            {
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                string myreader = "";
                string name = "";
                while (sqlite_datareader.Read())
                {
                    myreader = "";
                    name = "";

                    long col = DBFetch.GetColumnNumber(tablename);
                    for (int i = 0; i < 2; i++)
                    {
                        myreader += sqlite_datareader.GetString(i);
                    }
                    name = sqlite_datareader.GetString(2);
                    if (credentials == myreader)
                    {
                        sqlite_datareader.Close();
                        conn.Close();
                        return name;
                    }
                }
                sqlite_datareader.Close();

                conn.Close();
                return "";

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return "";
        }

        // It gets no of columns in a table 
        static public long GetColumnNumber(string tablename)
        {
            long noOfColumns=0;
            try 
            {
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                string columncount = $"SELECT COUNT(*) FROM pragma_table_info('{tablename}');";
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = columncount;
               noOfColumns = (Int64)sqlite_cmd.ExecuteScalar();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
          
            return noOfColumns;
        }

        // It reads the content from a particular note using note id 
        public static Note ReadNotesData(string ownerId, string sharedUserId, string noteId, string tablename)
        {
            Note note = new Note(" ", " ", " ");

            try
            {
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";

                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {
                    string userId = sqlite_datareader.GetString(0);
                    string nId = sqlite_datareader.GetString(1);
                  //  Console.WriteLine("CHECK : " + userId+ " " + ownerId + " " + nId + " " + noteId );
                    if (ownerId == userId && nId == noteId)
                    {
                      //  sharedUserId = ownerId + "_" + sharedUserId;
                        note = new Note(sharedUserId, sqlite_datareader.GetString(2), sqlite_datareader.GetString(3));
                    //    Console.WriteLine("INSIDE READING OF DATA and CHANGING THE USER ID "+note.user_Note_Id+ " " + ownerId + " " + sharedUserId + " " + noteId );
                        break; 
                    }

                }
                sqlite_datareader.Close();
                conn.Close();



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
          
            
            return note;
        }


        // It prints all the data of the user 
        public static void ReadAllData(string tablename)
        {
            
            try 
            
            {
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                long col = DBFetch.GetColumnNumber(tablename);
                while (sqlite_datareader.Read())
                {

                    string myreader = "";
                    for (int i = 0; i < col; i++)
                    {  if (i == 1)
                            continue;
                        myreader += sqlite_datareader.GetString(i);
                        if (i + 1 != col)
                            myreader += " | ";
                    }
                    Console.WriteLine(myreader);
                }
                sqlite_datareader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
         
        }


        // It prints all the contents of the notes from the notes table 
        static public void ReadContent(string tablename, string userId)
        {

            try 
            {
                Console.WriteLine("NoteId | Title");

                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                long col = DBFetch.GetColumnNumber(tablename);
                while (sqlite_datareader.Read())
                {
                    string id = sqlite_datareader.GetString(0);
                    string myreader = "";
                   // Console.WriteLine(id+userId);
                    if (id == userId)
                    {
                        myreader = sqlite_datareader.GetValue(1) + " | " + sqlite_datareader.GetString(2);
                        Console.WriteLine(myreader);
                    }
                }
                sqlite_datareader.Close();
                conn.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // It check whethere there is such a note id created by the user present or not 
       public static bool CheckNoteExists(string userId, long noteId, string tablename)
        { 
            try 
            {

                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {

                
                    string owner = sqlite_datareader.GetString(0);
                    long checkNoteId = (long)sqlite_datareader.GetValue(1);
                    if (checkNoteId == noteId)
                    { }
                    if (owner == userId && checkNoteId == noteId)
                    {
                        sqlite_datareader.Close();
                        conn.Close();
                        return true;
                    }
                }
                sqlite_datareader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false; 
        }

        // It gets all the details of the notes 
        public static bool NoteDetails(string userId, long noteId, string tablename)
        {
            try 
            {
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();

                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";
                sqlite_datareader = sqlite_cmd.ExecuteReader();


                long col = DBFetch.GetColumnNumber(tablename);
                while (sqlite_datareader.Read())
                {
                    string ownerId = sqlite_datareader.GetString(0);
                    long nId = (long)sqlite_datareader.GetValue(1);
                    string title = sqlite_datareader.GetString(2);
                    string content = sqlite_datareader.GetString(3);


                    if (nId == noteId && userId == ownerId)
                    {
    
                        Console.Write("\t\t\t Note Id : ");
                        Console.WriteLine($"\t {noteId} \t\t\t");
                        Console.Write("\t\t\t  Title  : ");
                        Console.WriteLine($"\t {title}");
                        Console.WriteLine("\t\t\t  Content  : \t\t\t");
                        Console.WriteLine($"\t\t\t {content} \t\t\t");

                        sqlite_datareader.Close();
                        conn.Close();
                        return true;
                    }
                }
               
                sqlite_datareader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
       
        }

        // It reads the data from the Shared Table user -> shared owner 
        public static void ReadSharedToNotes(string userId, string tablename)
        {
            try 
            {
              //  Console.WriteLine("\nSharedToUser Details : \n");
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";

                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {
                    string owner = "";
                    string myreader = "";

                    myreader += sqlite_datareader.GetString(1) + " " + sqlite_datareader.GetString(2);
                    owner = sqlite_datareader.GetString(0);

                    if (owner == userId)
                        Console.WriteLine(myreader);
                }
                sqlite_datareader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


        // It reads the data from the Shared Table shared owner -> user 
        public static void ReadSharedFromNotes(string userId, string tablename)
        {
            try
            {
               // Console.WriteLine("\nSharedFromUser Details : \n");
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                long col = DBFetch.GetColumnNumber(tablename);
                while (sqlite_datareader.Read())
                {
                    string owner = "";
                    string myreader = "";

                    myreader += sqlite_datareader.GetString(0) + " | " + sqlite_datareader.GetValue(2);
                    owner = sqlite_datareader.GetString(1);

                    if (owner == userId)
                        Console.WriteLine(myreader);
                }
                sqlite_datareader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        // Check if the Note is present in the Shared Table 
        public static string CheckNotesFromShared(string userId,string tablename, long noteId)
        {
            try
            {
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {

                    string  sharedUserId = sqlite_datareader.GetString(1);
                    long sharedNoteId = (long)sqlite_datareader.GetValue(2);
                    if (sharedUserId == userId && noteId == sharedNoteId)
                    {   string ownerId = sqlite_datareader.GetString(0);
                        sqlite_datareader.Close();
                        conn.Close();
                        return ownerId;
                    }
                }
                sqlite_datareader.Close();
                conn.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return "";

        }

        //Check if the user is a valid one 
        public static bool CheckValidUser(string sharedUserId, string tableName)
        {
            try
            {
                // Console.WriteLine("\nSharedFromUser Details : \n");
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tableName}";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    string owner = "";
                    owner = sqlite_datareader.GetString(0);

                    if (owner == sharedUserId)
                    {
                        sqlite_datareader.Close();
                        conn.Close();
                        return true;
                    }
                }
                sqlite_datareader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        //Check the if the user has notes shared from anyone 
        public static bool CheckSharedFromNotes(string userId, string tablename)
        {
            try
            {
                // Console.WriteLine("\nSharedFromUser Details : \n");
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    string owner = "";
                    owner = sqlite_datareader.GetString(1);

                    if (owner == userId)
                    {
                        sqlite_datareader.Close();
                        conn.Close();
                        return true;    
                    }
                }
                sqlite_datareader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false ; 
        }

    }
}
