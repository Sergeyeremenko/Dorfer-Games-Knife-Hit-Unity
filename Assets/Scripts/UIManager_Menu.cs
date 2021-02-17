using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_Menu : MonoBehaviour
{
    public void Game()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("Преход на игру");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Выход!");
    }
}
