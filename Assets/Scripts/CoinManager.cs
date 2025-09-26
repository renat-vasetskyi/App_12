using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> currentCoinsText;
    [SerializeField] GameObject lowCoinsPanel;

    private string coinsPrefsName = "Coins";
    public static CoinManager singleton;
    void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        UpdateCoinsText();
    }


    void Update()
    {

    }

    public void AddCoins(int coins)
    {
        int currentCoins = PlayerPrefs.GetInt(coinsPrefsName);
        SetCurrentCoins(currentCoins + coins);
    }
    public bool TryPurchase(int coins)
    {
        int currentCoins = PlayerPrefs.GetInt(coinsPrefsName);

        if (currentCoins < coins)
        {
            if (lowCoinsPanel)
            {
                lowCoinsPanel.SetActive(true);
            }
            return false; 
        }

        SetCurrentCoins(currentCoins - coins);
        return true;
    }

    public int GetCurrentCoins()
    {
        return PlayerPrefs.GetInt(coinsPrefsName);
    }

    public void SetCurrentCoins(int coins)
    {
        PlayerPrefs.SetInt(coinsPrefsName, coins);
        UpdateCoinsText();
    }

    public void UpdateCoinsText()
    {
        int currentCoins = PlayerPrefs.GetInt(coinsPrefsName);
        foreach (TextMeshProUGUI text in currentCoinsText)
        {
            text.text = currentCoins.ToString();
        }
    }
}
