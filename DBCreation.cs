using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YourNote
{
    public class DBCreation
    {
        public static string userTableName = "UserTable", notesTableName = "NotesTable", sharedTableName="ShareTable";
        static public void UserTableCreation()
        {
            // SQLiteConnection sqlite_conn = CreateConnection();
            CreateUserTable();
            //  sqlite_conn.Close();
        }

        static public void NotesTableCreation()
        {
            //  SQLiteConnection sqlite_conn = CreateConnection();
            CreateNotesTable();
            //  sqlite_conn.Close();

        }

        // Creates an object of SQLiteConnection 
        public static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
      
            // Create a new database connection:
            try
            {
                sqlite_conn = new SQLiteConnection("Data Source= database.db; Version = 3; New = True; Compress = True; ");

                // Open the connection:


                return sqlite_conn;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }


            return null;
        }

        //Creates The User Table 
        static public void CreateUserTable()
        {
            try 
            {
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                string table =
                    $"CREATE TABLE if not exists {userTableName}(UserId VARCHAR(10000) PRIMARY KEY,Password VARCHAR(10000),Name VARCHAR(10000))";
                //  create table Newsh1(uid integer primary key, ch varchar(10000));
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = table;
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

         
        }

        //Creates The Notes Table 

        static public void CreateNotesTable()
        {
            try 
            {

                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                // create table newsh(id integer, FOREIGN KEY(id) REFERENCES Newsh1(uid));
                SQLiteCommand sqlite_cmd;
                string table =
  $"CREATE TABLE if not exists {DBCreation.notesTableName}" +
  $"(UserId VARCHAR(10000)," +
  $"NoteId INTEGER PRIMARY KEY AUTOINCREMENT," +
  $"Title VARCHAR(10000)," +
  $"Content VARCHAR(1000), " +
  $"FOREIGN KEY(UserId) REFERENCES {DBCreation.userTableName}(UserId))";
                              sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = table;
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        //Creates The Shared Table 

        public static void SharedNotesTableCreation()
        {
            try 
            {

                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                string table =
      $"CREATE TABLE if not exists {sharedTableName}" +
      $"(OwnerId VARCHAR(10000)," +
      $"SharedUserId VARCHAR(10000)  " +
      $",SharedNoteId Integer," +
      $"PRIMARY KEY (OwnerId, SharedUserId, SharedNoteId)" +
      $" FOREIGN KEY(OwnerId) REFERENCES {userTableName}(UserId)" +
      $" FOREIGN KEY(SharedUserId) REFERENCES {userTableName}(UserId)" +
        $" FOREIGN KEY(SharedNoteId) REFERENCES {notesTableName}(NoteId))";

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = table;
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n");
            }
         
        }
    }
}
