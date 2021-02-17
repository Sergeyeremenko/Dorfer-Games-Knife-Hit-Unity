using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private const string SELECTED_KNIFE = "Knife";
    private const string HIGHSCORE = "Highscore";
    private const string TOTAL_APPLE = "TotalApple";
    private const string SOUND_SETTINGS = "SoundSettings";
    private const string VIBRATION_SETTING = "VibrationSettings";

    public bool IsGameOver { get; set; }
    public int Stage { get; set; }
    public int Score { get; set; }
    public Knife SelectedKnifePrefab { get; set; }
    public float ScreenHeight => Camera.main.orthographicSize * 2;
    public float ScreenWight => ScreenHeight / Screen.height * Screen.width;

    public int SelectedKnife
    {
        get => PlayerPrefs.GetInt(SELECTED_KNIFE, 0);
        set => PlayerPrefs.SetInt(SELECTED_KNIFE, value);
    }

    public int HighScore
    {
        get => PlayerPrefs.GetInt(HIGHSCORE, 0);
        set => PlayerPrefs.SetInt(HIGHSCORE, value);
    }

    public int TotalApple
    {
        get => PlayerPrefs.GetInt(TOTAL_APPLE, 0);
        set => PlayerPrefs.SetInt(TOTAL_APPLE, value);
    }

    public bool SoundSettings
    {
        get => PlayerPrefs.GetInt(SOUND_SETTINGS, 1) == 1;
        set => PlayerPrefs.SetInt(SOUND_SETTINGS, value ? 1 : 0);
    }

    public bool VibrationSettings
    {
        get => PlayerPrefs.GetInt(VIBRATION_SETTING, 1) == 1;
        set => PlayerPrefs.SetInt(VIBRATION_SETTING, value ? 1 : 0);
    }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
