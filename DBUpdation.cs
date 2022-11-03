using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YourNote;
    
namespace YourNote
{
    public class DBUpdation
    {
        //Creates new user 
        static public void InsertNewUser(User user)
        {
            try 
            {

                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"INSERT INTO {DBCreation.userTableName}(UserId, Password,Name) VALUES ('{user.Userid}', '{user.Password}','{user.Name}');";
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception )
            {
                Console.WriteLine("The User Already Exists");
            }
         
            //sqlite_cmd.CommandText = $"INSERT INTO {DBCreation.userTableName}(UserId, Password,Name) VALUES ('{user.Userid}' , ' " + { user.Password} + "','" + user.Name + "');";

        }

        //Creates new note 
        static public void InsertNewNote(Note newnote)
        {
            try
            {
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"INSERT INTO {DBCreation.notesTableName} (UserId, Title, Content) VALUES ('{newnote.userId}','{newnote.Title}','{newnote.Content}');";
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception)
            {
                Console.WriteLine("The Note already exists");
            }

  
        }

        //Creates a new entry for the shared note
        static public void InsertSharedNote(string ownerId, string shareduserId, long noteId)
        {
            try 
            {
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"INSERT INTO {DBCreation.sharedTableName} (OwnerId , SharedUserId, SharedNoteId) VALUES ('{ownerId}','{shareduserId}','{noteId}');";
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine("Notes Already Shared");
            }
          
        }


    }
}


// To make a SQLQuery return a value
//int yourValue = 0;
//string sql = "select day from time_limit where priority_level='p1'";
//using (SqlConnection conn = new SqlConnection(connString))
//{
//    SqlCommand cmd = new SqlCommand(sql, conn);
//    conn.Open();
//    yourValue = (Int32)cmd.ExecuteScalar();
//}


// To check if the table exists in the database or not 
//SELECT name FROM sqlite_master WHERE type='table' AND name='{table_name}';
//create table if not exists TableName(col1 typ1, ..., colN typN)