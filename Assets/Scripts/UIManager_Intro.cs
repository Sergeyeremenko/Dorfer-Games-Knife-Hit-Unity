using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager_Intro : MonoBehaviour
{
    public static UIManager_Intro Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Преход в Menu");
        //SoundManager.Instance.PlayButton();
        //SoundManager.Instance.PlayBossEndFight();
    }
}
