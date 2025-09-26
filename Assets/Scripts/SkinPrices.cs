using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinPrices", menuName = "Scriptable Objects/SkinPrices")]
public class SkinPrices : ScriptableObject
{
    [Header("Balloons")]
    public List<Prices> BalloonsSkins = new List<Prices>();

    [Header("Hats")]
    public List<Prices> HatsSkins = new List<Prices>();
    [System.Serializable]
    public struct Prices
    {
        public Sprite Skin;
        public int Price;
    }
}
