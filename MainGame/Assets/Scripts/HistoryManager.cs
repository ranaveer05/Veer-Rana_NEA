using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;

public class HistoryManager : MonoBehaviour
{

    public Text historyText;//  this will display History
    private string FolderPath = "C:/NEA Development/Cricket Game NEA";// DB location
    private string dbpath;// will be used as db path
    
    void Start()
    {
        dbpath = Path.Combine(FolderPath, "MainUsersDB.sqlite");// assigning Db path
        UserHistory();// loading history
    }

    
    public void UserHistory()// this will show history
    {
        int cntUser = SessionManager.Instance?.LoggedInUser ?? -1;// checks for user logged in
        if (cntUser == -1)// no user logged in then 
        {
            Debug.LogError("No logged-in user ID found.");// this will be shown
            historyText.text = "Error: No logged-in user.";// this will be shown in display
            return;// return to start
        }



        
        using (var connection = new SQLiteConnection("Data Source=" + dbpath + ";Version=3;"))
        {
            connection.Open();
            string link = @"
                SELECT gameDate, score 
                FROM scores 
                WHERE user_id = @id ORDER BY gameDate DESC;";// this is where the data will be shownn
            using(var command =  new SQLiteCommand(link,connection))
            {
                command.Parameters.AddWithValue("@id", cntUser);// assigning variable to User id which is logged in 

                using (var reader = command.ExecuteReader())
                {
                    if(!reader.HasRows)
                    {
                        historyText.text = "No Data";// this will be shown if no data found
                        return;
                    }

                    string display = "<b> Recent Games : \n </b>";// string to show words
                    while(reader.Read()) 
                    {
                        string date = reader["gameDate"].ToString();// assignes data from DB to variable
                        int scValue = Convert.ToInt32(reader["score"]);// assignes Data from DB to variable

                        display += $"Data : {date} - Score : {scValue}\n";// this will display 
                    }
                   Debug.Log(display);// this will display in console menu
                    historyText.text = display;// this will display histroy in Unity UI
                }
            }
        }
    }

    


    
}
