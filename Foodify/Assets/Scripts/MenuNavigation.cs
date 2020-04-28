using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{

    //Function that uses the scene manager to load a menu
    public void GoToMenuScene1()
    {
        SceneManager.LoadScene("Menu1");
    }
        
    public void GoToMenuScene2()
    {
        SceneManager.LoadScene("Menu2");
    }

    //Function that uses scene manager to load the home scene instead.
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("HomeScene");
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
