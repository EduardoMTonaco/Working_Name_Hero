using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerEndGame : MonoBehaviour
{
    public TowerLife Tower;
    public MenuController Menu;

    void Update()
    {
        CheckDeath();
    }

    public void CheckDeath()
    {
        if (Tower.stats.GetHealthPoints() <= 0)
        {
            Menu.ShowMenu(true, 0);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
