using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public Canvas MenuFull;

    public void PlayGame()
    {
        SceneManager.LoadScene("HeroMapaGrande");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GetMenu()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (MenuFull.isActiveAndEnabled)
            {
                ShowMenu(false, 1);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                ShowMenu(true, 0);
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }
    public void ShowMenu(bool activeMenu, float timescaleControler)
    {
        MenuFull.gameObject.SetActive(activeMenu);
        MainMenu.SetActive(activeMenu);
        Time.timeScale = timescaleControler;
    }

    public void ChangeSensibility()
    {
       
    }
}
