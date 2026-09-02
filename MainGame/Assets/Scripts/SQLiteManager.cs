using UnityEngine;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System;
using UnityEngine.SocialPlatforms.Impl;
using NUnit.Framework.Constraints;

public class SQLiteManager : MonoBehaviour
{
    private string dbpath; // variable 
    private string FolderPath = "C:/NEA Development/Cricket Game NEA"; // databse path 
    private int loggedInUserID = -1; // Stores the logged-in user's ID
   
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);// stays user logged in all scene
    }

    private void Start()
    {
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }
        dbpath = Path.Combine(FolderPath, "MainUsersDB.sqlite");// assigning dbpath to location of DB  and giving name to DB
        CreateDatabase();
    }

    private void CreateDatabase()// creates database if not exists 
    {
        if (!File.Exists(dbpath))// creates database if not exists 
        {
            SQLiteConnection.CreateFile(dbpath);
            using (var connection = new SQLiteConnection("Data Source=" + dbpath + ";Version=3;")) // assigning where to save database 
            {
                connection.Open(); // keep connection open 
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS users (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        username TEXT UNIQUE,
                        password TEXT
                    );
                    CREATE TABLE IF NOT EXISTS scores (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        user_id INTEGER,
                        username TEXT,
                        score INTEGER,
                        FOREIGN KEY(user_id) REFERENCES users(id)

                    );";// creates table is not exists and adds column defined above to save user information
                    
                    command.ExecuteNonQuery();
                }
            }
        }
    }

   

    public bool RegisterUser(string username, string password) // registers user and saves it on database
    {
        using (var connection = new SQLiteConnection("Data Source=" + dbpath + ";Version=3;")) // defines path
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO users (username, password) VALUES (@username, @password)";// insert data recieved while register and saves it on user table
                //saves recieved information in users table under username and password 
                command.Parameters.AddWithValue("@username", username);// data username recieved is assigned to variable to save into database
                command.Parameters.AddWithValue("@password", password);// data password recieved is assigned to varaible to save into database
                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false; // Username already exists
                }
            }
        }
    }



    public bool LoginUser(string username,string password) //for user to login and retirves data from database
    {
        using (var connection = new SQLiteConnection("Data Source=" + dbpath + ";Version=3;"))// assigned path 
        {
            connection.Open();
            string query = "SELECT id FROM users WHERE username = @username AND password = @password";
            // compares details that were provided while login in and check if detials matches
            using (var command = new SQLiteCommand(query,connection))
            {
                
                command.Parameters.AddWithValue("@username", username);//input detial varibale used to compare 
                command.Parameters.AddWithValue("@password", password);// input detail variable used to compare 
                
                object result = command.ExecuteScalar();

                
              
                if (result != null)// details are matched
                {
                    loggedInUserID = (int)(long)result; // gets user Id 
                    SessionManager.Instance.SetUserId(loggedInUserID);// send user id to session manager to keep user logged on in all scene
                    Debug.Log($"Login successful! User ID: {loggedInUserID}");// displays in console menu that login was successfull
                    return true;
                }
                else
                {
                    Debug.LogError("Invalid username or password.");// if deatails didnt match then this will show
                    return false;
                }
            }
        }
    }

    
    
}

