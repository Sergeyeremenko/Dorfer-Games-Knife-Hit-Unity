using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private ParticleSystem applePS;
    [SerializeField] private Text rewardText;

    private void Update()
    {
        if (RewardController.Instance.CanRewardNow())
        {
            rewardText.text = "Get Reward!";
        }
        else
        {
            TimeSpan timeToReward = RewardController.Instance.TimeToReward;
            rewardText.text = string.Format("{0:00}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
        }
    }

    public void RewardPlayer()
    {
        if (RewardController.Instance.CanRewardNow())
        {
            int amount = RewardController.Instance.GetRandomReward();
            StartCoroutine(RewardCR());
            RewardController.Instance.ResetRewardTime();
            SoundManager.Instance.PlayAppleReward();
            GameManager.Instance.TotalApple += amount;
        }
    }

    private IEnumerator RewardCR()
    {
        rewardPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        Instantiate(applePS);
        yield return new WaitForSeconds(3f);
        rewardPanel.SetActive(false);
    }
}
