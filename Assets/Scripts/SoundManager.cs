using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClip gameSoundClip;
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip appleHitClip;
    [SerializeField] private AudioClip knifeFireClip;
    [SerializeField] private AudioClip appleRewardClip;
    [SerializeField] private AudioClip wheelClip;
    [SerializeField] private AudioClip knifeClip;
    [SerializeField] private AudioClip unlockedClip;
    [SerializeField] private AudioClip fightStartClip;
    [SerializeField] private AudioClip fightendClip;

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
        DontDestroyOnLoad(gameObject);
    }

    private void PlaySound (AudioClip clip, float vol = 1)
    {
        if (GameManager.Instance.SoundSettings)
        {
            if (Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, vol);
            }
        }
    }
    public void PlayGameSound()
    {
        PlaySound(gameSoundClip);
    }
    public void PlayButton()
    {
        PlaySound(buttonClip);
    }
    public void PlayAppleHit()
    {
        PlaySound(appleHitClip);
    }
    public void PlayWheelHit()
    {
        PlaySound(wheelClip);
    }
    public void PlayKnifeHit()
    {
        PlaySound(knifeClip);
    }
    public void PlayUnlock()
    {
        PlaySound(unlockedClip);
    }
    public void PlayKnifeFire()
    {
        PlaySound(knifeFireClip);
    }
    public void PlayBossStartFight()
    {
        PlaySound(fightStartClip);
    }
    public void PlayBossEndFight()
    {
        PlaySound(fightendClip);
    }
    public void PlayGameOver()
    {
        PlaySound(gameOverClip);
    }
    public void PlayAppleReward()
    {
        PlaySound(appleRewardClip);
    }
    public void Vibrate()
    {
        if (GameManager.Instance.VibrationSettings)
        {
            Handheld.Vibrate();
        }
    }
}
