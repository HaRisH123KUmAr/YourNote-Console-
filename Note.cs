 using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourNote;

namespace YourNote
{
    public class Note
    {
        public string userId;
        public long noteId;
        public string Title;
        public string Content;
        

       public Note(string userId, string title, string content)
        {

            Title = title;
            this.userId = userId;
            Content = content;
        }
        public static void CreateNote(string userId)
        {
            Console.WriteLine("Enter the Title:");
            string pass = Console.ReadLine();
            Console.WriteLine("Enter the Content:");
            string name = Console.ReadLine();
            Note newnote = new Note(userId, pass, name);
            DBUpdation.InsertNewNote(newnote);
        }



       public void PrintAllNotes(string userId)
        {
            if (DBFetch.DataExist(DBCreation.notesTableName, userId) == false)
            {
                Console.WriteLine("No Notes Created Yet!");
                return;
            }
            Console.WriteLine("\t\t\tThe Notes are : \t\t\t");
            DBFetch.ReadContent(DBCreation.notesTableName, userId);
        }

        public void ViewNote(string userId)
        {
            Console.WriteLine("\t\t\tEnter the NoteId : \t\t\t");
            long noteId = Convert.ToInt64(Console.ReadLine());
           if( DBFetch.NoteDetails(userId ,noteId, DBCreation.notesTableName)== true)
            {
                Console.WriteLine("\n");
                return;
            }
            string ownerId = DBFetch.CheckNotesFromShared(userId, DBCreation.sharedTableName, noteId);
            if (ownerId.Length!=0)
            {
                Console.WriteLine("\t\t\tNotes Present In The Shared Folder\t\t\t");
                DBFetch.NoteDetails(ownerId, noteId, DBCreation.notesTableName);
            }
            else
            {
                Console.WriteLine("\t\t\tThere Is No Note Present in Both Shared and Personal Folder\t\t\t");
                Console.WriteLine("\t\t\tPlease Enter the Correct Note Id \t\t\t");
            }
            
               
            Console.WriteLine("\n");
        }

        public void ShareNotes(string ownerId)
        {
            Console.WriteLine("Enter the NoteId:");
            
           long noteId = Convert.ToInt64(Console.ReadLine());
             
            if (DBFetch.CheckNoteExists(ownerId, noteId, DBCreation.notesTableName) == false)
            {
                Console.WriteLine("No notes exists with such Note ID ");
                return;
            }
            Program pg = new Program();
            pg.PrintAllUsers();
            Console.WriteLine("Enter the UserId to whom be shared:");
            string sharedUserId = Console.ReadLine();
            if(DBFetch.CheckValidUser(sharedUserId, DBCreation.userTableName) == false)
            {
                Console.WriteLine("Invalid User Id");
                return;

            }
            if (sharedUserId == ownerId)
            {
                Console.WriteLine("You Cant Share Notes To Yourself");
                return;

            }
           // if()
            DBUpdation.InsertSharedNote(ownerId, sharedUserId, noteId);
          //  Note note = DBFetch.ReadNotesData(ownerId, sharedUserId, noteId, DBCreation.notesTableName);
          //  DBUpdation.InsertNewNote(note);
        }

        public void ViewSharedNotes(string userId)
        {
            if (DBFetch.CheckSharedFromNotes(userId, DBCreation.sharedTableName) == false)
            {
                Console.WriteLine("No Notes Are Shared To You");
                return;
            }

            Console.WriteLine("\t\t\tThe Notes are : \t\t\t");
            Console.WriteLine("Shared From ID | Note");
            DBFetch.ReadSharedFromNotes(userId, DBCreation.sharedTableName);

        }
    }
}



