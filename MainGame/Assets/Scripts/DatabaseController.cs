using UnityEngine;
using UnityEngine.UI;


public class DatabaseController : MonoBehaviour
{

    public InputField loginEmail, loginPassword, registerEmail, registerPassword, registerCPassword, registerUsername,forgetPassword;

    public GameObject LoginPanel, RegisterPanel, forgetPasswordPanel, notification;

    public Text notifTitle, notifMessage;

    public Toggle Remember;

    private string connection;

    private void Start()
    {
       
        connection=$"Provider=Microsoft.ACE.OLEDB.12.0;Data Source =  "; 
    }


    public void OpenLoginPage()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }
    public void OpenRegisterPage()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
        forgetPasswordPanel.SetActive(false);
    }
    public void OpenforgetPassword()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(false);
        forgetPasswordPanel.SetActive(true);
    }
    public void Login()
    {
            if (string.IsNullOrEmpty(loginEmail.text) && string.IsNullOrEmpty(loginPassword.text))
        {
            NotifError("Error", "Field Empty");
            return;
        }
    }
    public void RegisterUser()
    {
        if (string.IsNullOrEmpty(registerEmail.text) && string.IsNullOrEmpty(registerPassword.text)&& string.IsNullOrEmpty(registerCPassword.text)&& string.IsNullOrEmpty(registerUsername.text))
        {
            NotifError("Error", "Field Empty");
            return;
        }
    }
    public void forgetPass()
    {
        if (string.IsNullOrEmpty(forgetPassword.text))
        {
            NotifError("Error" , "Field Empty");
            return;
        }
    }
    private void NotifError(string title, string message)
    {
        notifTitle.text = ""+title;
        notifMessage.text = ""+message;
        
        notification.SetActive(true);
    }
    public void CloseNotif()
    {
        notifTitle.text = "";
        notifMessage.text = "";
        notification.SetActive(false);
    }
}
