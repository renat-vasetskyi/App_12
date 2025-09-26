
using System;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class StoreCard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Image SkinImage;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Button PriceBtn;
    [SerializeField] Button UseBtn;

    private int price;
    CardType cardType;

    private static Button preiousHatUseButton=null;
    private static Button preiousBalloonUseButton=null;

    [System.Serializable]
    public enum CardType
    {
        Balloon, Hat, Booster
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Init(Sprite skin, int price_,CardType type)
    {
        if (SkinImage)
        {
            SkinImage.sprite = skin;
            SkinImage.SetNativeSize();
        }
        if (priceText)
        {
            priceText.text = price_.ToString();
        }
        cardType = type;
        price = price_;

        if (cardType == CardType.Balloon || cardType == CardType.Hat)
        {
            if (PlayerPrefs.GetInt(skin.name) == 1 || price_ == 0)
            {
                EnableUseButton();
            }
            else
            {
                EnablePriceButton();
            }
        }
    }

    public void SetCardType(CardType cardType_)
    {
        cardType = cardType_;
    }

    private void EnableUseButton()
    {
        if (PriceBtn)
            PriceBtn.gameObject.SetActive(false);

        if (UseBtn)
        {
            UseBtn.gameObject.SetActive(true);

            if (PlayerPrefs.GetString("BalloonSkin") == SkinImage.sprite.name || PlayerPrefs.GetString("HatSkin") == SkinImage.sprite.name)
            {
                UseBtn.interactable = false;
                if (cardType == CardType.Balloon)
                {
                    preiousBalloonUseButton = UseBtn;
                }
                if (cardType == CardType.Hat)
                {
                    preiousHatUseButton = UseBtn;
                }
            }
            else
            {
                UseBtn.interactable = true;
            }

        }
    }

    private void EnablePriceButton()
    {
        if (PriceBtn)
            PriceBtn.gameObject.SetActive(true);

        if (UseBtn)
            UseBtn.gameObject.SetActive(false);
    }

    public void Purchase()
    {
        if (Store.singleton.TryPurchase(price, SkinImage.sprite.name))
        {
            EnableUseButton();
        }
    }

    public void UseSkin()
    {
        if (cardType == CardType.Balloon)
        {
            PlayerPrefs.SetString("BalloonSkin", SkinImage.sprite.name);
            if (preiousBalloonUseButton)
            {
                Debug.Log(preiousBalloonUseButton.transform.parent.name);
                preiousBalloonUseButton.interactable = true;
            }
            if (UseBtn)
            {
                UseBtn.interactable = false;
                preiousBalloonUseButton = UseBtn;
            }
        }
        if (cardType == CardType.Hat)
        {
            PlayerPrefs.SetString("HatSkin", SkinImage.sprite.name);
            if (preiousHatUseButton)
            {
                Debug.Log(preiousHatUseButton.transform.parent.name);
                preiousHatUseButton.interactable = true;
            }
            if (UseBtn)
            {
                UseBtn.interactable = false;
                preiousHatUseButton = UseBtn;
            }
        }

        
            
        
    }   
}
