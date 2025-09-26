using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorial;
    [SerializeField] LevelManager levelManager;
    Rigidbody2D rb;
    void Start()
    {
        if (PlayerPrefs.GetInt("TutorialPassed") == 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            rb = player.GetComponent<Rigidbody2D>();
            rb.simulated = false;
            tutorial.SetActive(true);
            PlayerPrefs.SetInt("TutorialPassed", 1);

            if (levelManager)
            {
                levelManager.SetActiveButtonsToDisable(false);
            }
        }
        else
        {
            tutorial.SetActive(false);
        }
    }

    public void CloseTutorial()
    {
        if (levelManager)
        {
            levelManager.SetActiveButtonsToDisable(true);
            tutorial.SetActive(false);
            rb.simulated = true;
        }
    }
    
}
