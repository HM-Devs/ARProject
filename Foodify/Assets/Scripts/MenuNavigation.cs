//This script is completely unique and created by the author. 

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

    public void GoToMenuScene3()
    {
        SceneManager.LoadScene("Menu3");
    }

    public void GoToMenuScene4()
    {
        SceneManager.LoadScene("Menu4");
    }

    public void GoToMenuScene5()
    {
        SceneManager.LoadScene("Menu5");
    }

    public void GoToMenuScene6()
    {
        SceneManager.LoadScene("Menu6");
    }

    public void GoToMenuScene7()
    {
        SceneManager.LoadScene("Menu7");
    }

    public void GoToMenuScene8()
    {
        SceneManager.LoadScene("Menu8");
    }

    public void GoToMenuScene9()
    {
        SceneManager.LoadScene("Menu9");
    }

    public void GoToMenuScene10()
    {
        SceneManager.LoadScene("Menu10");
    }

    public void GoToMenuScene11()
    {
        SceneManager.LoadScene("Menu11");
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
