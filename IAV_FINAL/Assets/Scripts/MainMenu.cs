using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject help;
    // Start is called before the first frame update
    void Start()
    {
        help.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Aulas");
    }

    public void Help()
    {
        menu.SetActive(false);
        help.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        menu.SetActive(true);
        help.SetActive(false);
    }
}
