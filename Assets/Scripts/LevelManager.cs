using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{

    [SerializeField] private GameObject player;

    [SerializeField] GameObject winPanel;
    [SerializeField] TextMeshProUGUI winPanelScore;
    [SerializeField] GameObject losePanel;

    [SerializeField] GameObject LoadindScreeen;

    [SerializeField] List<Button> buttonsToDisable = new List<Button>();

    void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (winPanel)
            winPanel.SetActive(false);

        if (losePanel)
            losePanel.SetActive(false);
    }

    void Update()
    {

    }

    public void StopGameByLose()
    {
        if (losePanel)
            losePanel.SetActive(true);

        if (player)
        {
            // player.SetActive(false);
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.simulated = false;
        }
        SetActiveButtonsToDisable(false);
        int score = player.GetComponent<PlayerManager>().GetScore();
        if (winPanelScore)
        {
            winPanelScore.text = score.ToString();
        }
        string levelScoreName = SceneManager.GetActiveScene().name + "Score";
        if (PlayerPrefs.GetInt(levelScoreName) < score)
        {
            PlayerPrefs.SetInt(levelScoreName, score);
        }
    }

    public void StopGameByWin()
    {
        if (winPanel)
            winPanel.SetActive(true);

        if (player)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.simulated = false;
        }
        SetActiveButtonsToDisable(false);
        int score = player.GetComponent<PlayerManager>().GetScore();
        if (winPanelScore)
        {
            winPanelScore.text = score.ToString();
        }
        string levelScoreName = SceneManager.GetActiveScene().name + " Score";
        if (PlayerPrefs.GetInt(levelScoreName) < score)
        {
            PlayerPrefs.SetInt(levelScoreName, score);
        }
    }

    public void LoadMenu()
    {
        LoadScene(0);
    }

    public void LoadScene(int buildIndex)
    {
        if (LoadindScreeen)
            LoadindScreeen.gameObject.SetActive(true);

        StartCoroutine(Loading(buildIndex));
    }

    IEnumerator Loading(int buildIndex)
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(buildIndex);
        loadAsync.allowSceneActivation = false;

        while (!loadAsync.isDone)
        {
            if (loadAsync.progress >= 0.9f && !loadAsync.allowSceneActivation)
            {
                //yield return new WaitForSeconds(0.1f);
                loadAsync.allowSceneActivation = true;
            }
            yield return null;
        }
    }


    public void ReloadCurrentLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        LoadScene(level);
    }

    public void loadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
            nextIndex = 0;

        LoadScene(nextIndex);
    }

    public void SetActiveButtonsToDisable(bool active)
    {
        foreach (var btn in buttonsToDisable)
        {
            btn.interactable=active;
        }
    }
}
