using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsGridGenerator : MonoBehaviour
{
    [SerializeField] GameObject PasedLevelCardPrefab;
    [SerializeField] GameObject PlayLevelCardPrebab;
    [SerializeField] GameObject LevelGrid;
    [SerializeField] LevelManager levelManager;
    [SerializeField] int FirstLevelIndex=1;
    [SerializeField] int LastLevelIndex=3;

    [SerializeField] int InaccessibleLevelsAmount = 3;
    [SerializeField] GameObject InaccessibleLevelCardPrebab;

    private bool PLayLevelCreated = false;

    void Start()
    {
        GameObject cardToSpawn = PasedLevelCardPrefab;
        for (int i = FirstLevelIndex; i <= LastLevelIndex; i++)
        {
            int score = PlayerPrefs.GetInt($"Level {i} Score");
            if (score == 0 && !PLayLevelCreated)
            {
                cardToSpawn = PlayLevelCardPrebab;
                PLayLevelCreated = true;
            }

            GameObject card = Instantiate(cardToSpawn, LevelGrid.transform);
            LevelCard levelCard = card.GetComponent<LevelCard>();
            levelCard.Init(i, score, i, levelManager);

            if (PLayLevelCreated)
            {
                InaccessibleLevelsAmount += InaccessibleLevelsAmount - i;
                break;
            }
            
        }

        for (int j = 0; j < InaccessibleLevelsAmount; j++)
        {
            GameObject card = Instantiate(InaccessibleLevelCardPrebab, LevelGrid.transform);
        }
    }
}
