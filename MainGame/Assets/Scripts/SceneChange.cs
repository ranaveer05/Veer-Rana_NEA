using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour
{

    public string sceneName;// variable to go to next scene 
    

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);// when this function is called the scene change and which scene will be changed into depends on variable 
    }
}
