using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Settings")]

    [SerializeField] private Text scoreText;
    [SerializeField] private Text stageText;

    [SerializeField] private GameObject stageContainer;

    [SerializeField] private Color stageCompletedColor;
    [SerializeField] private Color stageNormalColor;
    public List<Image> stageIcons;

    [Header("UI BOSS")]
    [SerializeField] private GameObject bossFight;
    [SerializeField] private GameObject bossDefeate;

    [Header("GameOver UI")]
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private Text gameOverScore;
    [SerializeField] private Text gameOverStage;

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
        scoreText.text = GameManager.Instance.Score.ToString();
        gameOverScore.text = GameManager.Instance.Score.ToString();

        stageText.text = "Stage " + GameManager.Instance.Stage;
        gameOverStage.text = "Stage " + GameManager.Instance.Stage;

        UpdateUI();
    }

    public IEnumerator BossStart()
    {
        SoundManager.Instance.PlayBossStartFight();
        bossFight.SetActive(true);
        yield return new WaitForSeconds(1f);
        bossFight.SetActive(false);
    }
    public IEnumerator BossDefeated()
    {
        SoundManager.Instance.PlayBossEndFight();
        bossDefeate.SetActive(true);
        yield return new WaitForSeconds(1f);
        bossDefeate.SetActive(false);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        stageContainer.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Home()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OpenFacebook()
    {
        Application.OpenURL("https://vk.com/itpedia_youtube");
        SoundManager.Instance.PlayButton();
    }

    public void OpenShop()
    {
        GeneralUI.Instance.OpenShop();
        SoundManager.Instance.PlayButton();
    }

    public void OpenOptions()
    {
        GeneralUI.Instance.OpenSettings();
        SoundManager.Instance.PlayButton();
    }

    private void UpdateUI()
    {
        if (GameManager.Instance.Stage % 5 == 0)
        {
            foreach (var icon in stageIcons)
            {
                icon.gameObject.SetActive(false);

                stageIcons[stageIcons.Count - 1].color = stageNormalColor;
                stageText.text = "Boss " + LevelManager.Instance.BossName;
            }
        }
        else
        {
            for (int i = 0; i < stageIcons.Count; i++)
            {
                stageIcons[i].gameObject.SetActive(true);
                stageIcons[i].color = GameManager.Instance.Stage % 5 <= i ? stageNormalColor : stageCompletedColor;
            }
        }
    }
}
