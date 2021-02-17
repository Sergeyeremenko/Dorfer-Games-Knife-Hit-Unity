using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private KnifeItem itemPrefab;
    [SerializeField] private GameObject shopContainer;

    [Header("Knifes")]
    [SerializeField] private Text counter;
    [SerializeField] private Text price;

    [Header("Knifes")]
    [SerializeField] private Image knifeUnlocked;
    [SerializeField] private Image knifeLocked;

    [SerializeField] private MainManager mainManager;

    private List<KnifeItem> shopItems;

    public Knife[] knifeList;
    public KnifeItem KnifeItemSelected { get; set; }

    private KnifeItem SelectedItem
    {
        get { return shopItems.Find(x => x.IsSelected); } // match:
    }

    private void Start()
    {
        Setup();
    }
    private void Update()
    {
        price.text = SelectedItem.Price.ToString();
    }

    private void Setup()
    {
        shopItems = new List<KnifeItem>();
        for (int i = 0; i < knifeList.Length; i++)
        {
            KnifeItem item = Instantiate(itemPrefab, shopContainer.transform);
            item.Setup(i, this);
            shopItems.Add(item);
        }
        shopItems[GameManager.Instance.SelectedKnife].OnClick();
    }

    public void UnlockKnife()
    {
        if (GameManager.Instance.TotalApple > SelectedItem.Price)
        {
            GameManager.Instance.TotalApple -= SelectedItem.Price;
            SoundManager.Instance.PlayUnlock();
            SelectedItem.IsUnlocked = true; //true
            SelectedItem.UpdateUI();
            GameManager.Instance.SelectedKnife = SelectedItem.Index;
            
            UpdateShopUI();
            SoundManager.Instance.PlayButton();
        }
    }

    public void UpdateShopUI()
    {
        knifeUnlocked.sprite = SelectedItem.KnifeImage.sprite;
        knifeLocked.sprite = SelectedItem.KnifeImage.sprite;

        knifeUnlocked.gameObject.SetActive(SelectedItem.IsUnlocked);
        knifeLocked.gameObject.SetActive(!SelectedItem.IsUnlocked);

        int itemsUnlocked = 0;
        itemsUnlocked = shopItems.FindAll(x => x.IsUnlocked).Count;

        counter.text = itemsUnlocked + "/" + knifeList.Length;

        GameManager.Instance.SelectedKnifePrefab = knifeList[GameManager.Instance.SelectedKnife];

        if (mainManager != null)
        {
            mainManager.SelectedKnife.sprite = GameManager.Instance.SelectedKnifePrefab.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
