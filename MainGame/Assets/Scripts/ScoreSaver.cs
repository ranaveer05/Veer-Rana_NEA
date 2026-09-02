using System.IO;
using UnityEngine;
using System.Data;
using System.Data.SQLite;
using UnityEngine.SocialPlatforms.Impl;
using NUnit.Framework.Interfaces;
using System;
using System.Data.Common;

public class ScoreSaver : MonoBehaviour
{
    
    private string dbPath = "C:/NEA Development/Cricket Game NEA";
    private string dbpath;

    private void Awake()
    {
        
        dbpath = Path.Combine(dbPath, "MainUsersDB.sqlite");// assigns path to database with database to use
    }
    public void SaveScore(int userId,int runs)
    {
        if (userId <= 0)// checks for user id persence 
        {
            Debug.LogError("Invalid User ID. Cannot save score.");// no user id is logged in then this message will pop
            return;
        }

        if (runs <= 0)// check for run scored 
        {
            Debug.LogWarning("Score is zero. No need to save.");// no runs scored the this will pop up
            return;
        }

        SaveToDB( userId, runs);// if both exist then saved in Database
    }

    public void SaveToDB( int userId,  int runs)// variable are created 
    {
       
        userId = SessionManager.Instance.LoggedInUser;// variable is assigned with logged in user 
      
        runs = GameManager.Instance.totalruns;// variable is assigned with total runs
      
        try
        {
            using (var connection = new SQLiteConnection("Data Source=" + dbpath + ";Version=3;"))// defines database path
            {
                connection.Open();
                    string query = "INSERT INTO scores (user_id,username,score,gameDate) SELECT u.id, u.username, @score, CURRENT_DATE FROM  users u WHERE u.id=@playerId";
                //inserts variable into scores table and also adding new column 
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        
                        command.Parameters.AddWithValue("@playerId", userId);// assigns variable to use in SQLite

                        command.Parameters.AddWithValue("@score", runs);// assigns variable to use in SQLite

                    command.ExecuteNonQuery();// executes command
                    }
                }
        }
        catch (Exception ex)
        {
            Debug.LogError(" Database Error: " + ex.Message);// if anyhting goes wrong then this message will pop up
        }
              
    }
}
    

    


