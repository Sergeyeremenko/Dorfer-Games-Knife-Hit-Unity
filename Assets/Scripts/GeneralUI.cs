using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUI : MonoBehaviour
{
    public static GeneralUI Instance;

    [Header("Panels")]
    [SerializeField] private GameObject settingsPannel;
    [SerializeField] private GameObject shopPanel;

    [Header("UI")]
    [SerializeField] private GameObject soundsOn;
    [SerializeField] private GameObject soundsOff;
    [SerializeField] private GameObject vibrateOn;
    [SerializeField] private GameObject vibrateOff;
    [SerializeField] private Text totalApple;

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

    private void Update()
    {
        totalApple.text = GameManager.Instance.TotalApple.ToString();
        UpdateSoundsUI();
        UpdateVibrationUI();
    }

    public void SoundsOnOff()
    {
        SoundManager.Instance.PlayButton();
        if (GameManager.Instance.SoundSettings)
        {
            GameManager.Instance.SoundSettings = false;
        }
        else
        {
            GameManager.Instance.SoundSettings = true;
        }
    }

    public void VibrateOnOff()
    {
        SoundManager.Instance.PlayButton();
        if (GameManager.Instance.VibrationSettings)
        {
            GameManager.Instance.VibrationSettings = false;
        }
        else
        {
            GameManager.Instance.VibrationSettings = true;
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        Debug.Log("Открытие панели магазина");
        SoundManager.Instance.PlayButton();
        Debug.Log("Воспроизведение звука кнопки");
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Debug.Log("Закрытие панели магазина");
        SoundManager.Instance.PlayButton();
        Debug.Log("Воспроизведение звука кнопки");
    }

    public void OpenSettings()
    {
        settingsPannel.SetActive(true);
        Debug.Log("Открытие панели настроек");
        SoundManager.Instance.PlayButton();
        Debug.Log("Воспроизведение звука кнопки");
    }

    public void CloseSettings()
    {
        settingsPannel.SetActive(false);
        Debug.Log("Закрытие панели настроек");
        SoundManager.Instance.PlayButton();
        Debug.Log("Воспроизведение звука кнопки");
    }

    private void UpdateSoundsUI()
    {
        if (GameManager.Instance.SoundSettings)
        {
            soundsOn.SetActive(true);
            soundsOff.SetActive(false);
        }
        else
        {
            soundsOn.SetActive(false);
            soundsOff.SetActive(true);
        }
    }

    private void UpdateVibrationUI()
    {
        if (GameManager.Instance.VibrationSettings)
        {
            vibrateOn.SetActive(true);
            vibrateOff.SetActive(false);
        }
        else
        {
            vibrateOn.SetActive(false);
            vibrateOff.SetActive(true);
        }
    }
}
