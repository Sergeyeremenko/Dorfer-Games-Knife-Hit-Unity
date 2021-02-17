using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class knifeCounter : MonoBehaviour
{
    public static knifeCounter Instance;

    [SerializeField] private GameObject knifeSprite;
    [SerializeField] private Color knifeReadyColor;
    [SerializeField] private Color knifeWastedClolor;

    private List<GameObject> icons = new List<GameObject>();

    private void Awake()
    {
        if (Instance)
        {
            //Destroy(gameObject); // Не Включать!!) иначе хана)
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetupKnife(int amount)
    {
        foreach (var icon in icons)
        {
            Destroy(icon);
        }

        icons.Clear();

        for(int i = 0; i < amount; i++)
        {
            GameObject icon = Instantiate(knifeSprite, transform);
            icon.GetComponent<Image>().color = knifeReadyColor;
            icons.Add(icon);
        }
    }
    public void KnifeHit(int amount)
    {
        for (int i = 0; i < icons.Count; i++)
        {
           icons[i].GetComponent<Image>().color = i < amount ? knifeWastedClolor : knifeReadyColor;
        }
    }
}
