using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgresSlider : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform finish;
    [SerializeField] TextMeshProUGUI passedDistText;
    [SerializeField] TextMeshProUGUI overallDistText;
    [SerializeField] Slider slider;
    private int maxDist;
    void Awake()
    {

    }
    void Start()
    {
        if (!player)
        { player = GameObject.FindGameObjectWithTag("Player").transform; }
        if (!finish)
            finish = GameObject.FindGameObjectWithTag("Finish").transform;

        if (!player || !finish)
            return;

        int dist = (int)Vector2.Distance(player.position, finish.position);
        maxDist = dist;

        if (slider)
        {
            slider.value = 0;
            slider.minValue = 0;
            slider.maxValue = dist;
        }

        if (passedDistText)
            passedDistText.text = "0 m";

        if (overallDistText)
            overallDistText.text = $"{dist} m";

        StartCoroutine(CountDistance());
    }


    private IEnumerator CountDistance()
    {
        if (!player || !finish)
            yield return null;
        else
        {
            while (true)
            {
                float dist = Vector2.Distance(player.position, finish.position);

                if (slider)
                {
                    slider.value = maxDist - dist;
                }

                if (passedDistText)
                    passedDistText.text = $"{maxDist - (int)dist} m";
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
