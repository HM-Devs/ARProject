using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToMenuScene1()
    {
        SceneManager.LoadScene("Menu1");
    }

    public void GoToMenuScene2()
    {
        SceneManager.LoadScene("Menu2");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
