using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] private Image selectedKnife;

    private string gameURL = "https://play.google.com/store/apps/details?id=...."; // ����� ����
    private string appID = "id...."; // ������� id..

    public Image SelectedKnife => selectedKnife;
    public void Rate()
    {
        Application.OpenURL(gameURL + appID);
        SoundManager.Instance.PlayButton();
        Debug.Log("������ ������");
    }
    public void Play()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("������ �� ����");
        SoundManager.Instance.PlayButton();
        SoundManager.Instance.PlayGameSound();
        Debug.Log("������ ������");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("�����!");
    }
}
