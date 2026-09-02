using UnityEngine;
using UnityEngine.UI;

public class LoginRegister : MonoBehaviour
{
    public SQLiteManager dbManager; // to user SQLiteManager 

    
    public GameObject loginRegisterPanel; // game object
    public InputField usernameInput;// input field where user will put username 
    public InputField passwordInput;// input feild where user will put password
    public Text feedbackText;//to show messages 

   
   
    private void Start()
    {
        dbManager = FindFirstObjectByType<SQLiteManager>(); // this script is tied up with SQLiteManager 
        ShowLoginUI();// Unity Ui is turned on 
    }

    public void Register()
    {
        bool success = dbManager.RegisterUser(usernameInput.text, passwordInput.text);//send typed in details to SQLiteManager to register user 
        feedbackText.text = success ? "Registration Successful!" : "Username already exists!";// text to show if registration was success or not 
    }

    public void Login()
    {
        bool success = dbManager.LoginUser(usernameInput.text, passwordInput.text);// send typed in information to SQLiteManager to compare with databse to check if user exist
        if (success)
        {
            feedbackText.text = "Login Successful!";// text to show if login was success 
            
        }
        else
        {
            feedbackText.text = "Invalid Username or Password!";// text to show if login was unsuccessful
        }
    }
    private void ShowLoginUI()
    {
        loginRegisterPanel.SetActive(true);// turns on ui panel to login and register 
        
    }

    
}
