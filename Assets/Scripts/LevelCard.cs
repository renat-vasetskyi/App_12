using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TextMeshProUGUI LevelNumText;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] Button button;

    private int SceneToLoad;

    private LevelManager levelManager;
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {

    }

    public void Init(int levelNum, int score, int sceneIndex, LevelManager levelManager_)
    {
        if (LevelNumText)
        {
            LevelNumText.text = levelNum.ToString();
        }
        SceneToLoad = sceneIndex;
        levelManager = levelManager_;


        if (ScoreText)
        {
            if (score > 0)
                ScoreText.text = score.ToString();
            else
            {
                ScoreText.text = "PLAY";
            }
        }
    }

    public void LoadLevel()
    {
        if (levelManager == null)
            return;

        Debug.Log(SceneToLoad);
        levelManager.LoadScene(SceneToLoad + 1);
    }
}
