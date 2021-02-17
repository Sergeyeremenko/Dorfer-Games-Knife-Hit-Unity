using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeItem : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image knifeImage;

    [SerializeField] private GameObject selectedImage;

    [Header("Colors")]
    [SerializeField] private Color knifeUnlockColor;
    [SerializeField] private Color knifeLockColor;
    [SerializeField] private Color knifeUnlockBackgroundColor;
    [SerializeField] private Color knifeLockBackgroundColor;

    [SerializeField] private int price;

    private Shop shop;
    private Knife knife;

    [SerializeField] private GameObject ButtonUnlock; // кнопка покупки

    private const string KNIFE_UNLOCKED = "KnifeUnlocked_";

    public int Index { get; set; }

    public bool IsUnlocked
    {
        get
        {
            if (Index == 0)
            {
                return true;
            }
            return PlayerPrefs.GetInt(KNIFE_UNLOCKED + Index, 0) == 1;
        }

        set
        {
            PlayerPrefs.SetInt(KNIFE_UNLOCKED + Index, value ? 1 : 0);
        }
    }

    public bool IsSelected
    {
        get => selectedImage.activeSelf;
        set
        {
            if (value)
            {
                if (shop.KnifeItemSelected != null)
                {
                    shop.KnifeItemSelected.IsSelected = false;
                }
                shop.KnifeItemSelected = this;
            }
            selectedImage.SetActive(value);
        }
    }

    public int Price => price;
    public Image KnifeImage => knifeImage;

    public void Setup (int index, Shop shop)
    {
        this.shop = shop;
        Index = index;
        knife = this.shop.knifeList[Index];
        knifeImage.sprite = knife.GetComponent<SpriteRenderer>().sprite;
        UpdateUI();
    }

    public void OnClick()
    {
        if (IsUnlocked && IsSelected)
        {
            //GeneralUI.Instance.CloseShop(); // если что разблокировать!!
            //ButtonUnlock.SetActive(false);
        }

        if (!IsSelected)
        {
            IsSelected = true;
        }

        if (IsUnlocked)
        {
            GameManager.Instance.SelectedKnife = Index;
            GeneralUI.Instance.CloseShop();
            //ButtonUnlock.SetActive(true);
            // сюда нужно отключать кнопку покупки при уже купленном
            //GeneralUI.Instance.CloseShop(); // в этом случае - меню магаза - открывается со второго раза, но покупка может дублироваться - при первой покупки..
        }
        shop.UpdateShopUI();
    }

    public void UpdateUI()
    {
        backgroundImage.color = IsUnlocked ? knifeUnlockBackgroundColor : knifeLockBackgroundColor;
        knifeImage.GetComponent<Mask>().enabled = !IsUnlocked;

        knifeImage.transform.GetChild(0).GetComponent<Image>().color = IsUnlocked ? knifeUnlockColor : knifeLockColor;
        knifeImage.transform.GetChild(0).gameObject.SetActive(!IsUnlocked);
    }
}
