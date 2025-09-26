using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Store : MonoBehaviour
{
    [Header("Balloons")]
    public List<Prices> BalloonsSkins = new List<Prices>();

    public List<GameObject> BallonsSkinsGrid = new List<GameObject>();
    public int skinsInGrid = 9;

    [SerializeField] GameObject BalloonSkinCardPrefab;

    [Header("Hats")]
    public List<Prices> HatsSkins = new List<Prices>();
    public List<GameObject> HatsSkinsGrid = new List<GameObject>();

    [SerializeField] GameObject HatSkinCardPrefab;

    [Header("Booster")]
    [SerializeField] Prices Booster;
    [SerializeField] GameObject BoosterCardPrefab;

    public static Store singleton;

    [System.Serializable]
    public struct Prices
    {
        public Sprite Skin;
        public int Price;
    }
    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        if (!BalloonSkinCardPrefab)
            return;

        int skinsCount = BalloonsSkins.Count;
        int i = 0;
        foreach (GameObject grid in BallonsSkinsGrid)
        {
            for (int j = 0; j < skinsInGrid; j++)
            {
                int skinNum = j + skinsInGrid * i;
                if (skinNum >= skinsCount)
                    break;

                GameObject Obj = Instantiate(BalloonSkinCardPrefab, grid.transform);
                if (Obj.TryGetComponent<StoreCard>(out var card))
                {
                    card.Init(BalloonsSkins[skinNum].Skin, BalloonsSkins[skinNum].Price, StoreCard.CardType.Balloon);
                }
            }
            i++;
        }

        skinsCount = HatsSkins.Count;
        i = 0;
        foreach (GameObject grid in HatsSkinsGrid)
        {
            for (int j = 0; j < skinsInGrid; j++)
            {
                int skinNum = j + skinsInGrid * i;
                if (skinNum < skinsCount)
                {
                    GameObject Obj = Instantiate(HatSkinCardPrefab, grid.transform);
                    if (Obj.TryGetComponent<StoreCard>(out var card))
                    {
                        card.Init(HatsSkins[skinNum].Skin, HatsSkins[skinNum].Price, StoreCard.CardType.Hat);
                    }
                }
                else
                {
                    GameObject Obj = Instantiate(BoosterCardPrefab, grid.transform);
                    if (Obj.TryGetComponent<StoreCard>(out var card))
                    {
                        card.Init(Booster.Skin, Booster.Price, StoreCard.CardType.Booster);
                    }
                    break;
                }
            }
            i++;
        }


    }

    public bool TryPurchase(int price, String skinName)
    {
        CoinManager manager = CoinManager.singleton;
        if (!manager)
            return false;

        if (manager.TryPurchase(price))
        {
            PlayerPrefs.SetInt(skinName, 1);
            return true;
            /*switch (cardType)
            {
                case StoreCard.CardType.Balloon:


                    break;

            }*/
        }
        return false;
    }
    
}
