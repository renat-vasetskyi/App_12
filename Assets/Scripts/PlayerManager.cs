using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] LevelManager levelManager;

    [SerializeField] float sidePushForce = 1;

    [Header("Hat")]
    [SerializeField] Sprite HatSprite;
    [SerializeField] SpriteRenderer HatSocket;

    [Header("Coins")]
    [SerializeField] int minCoincsPerCollect = 30;
    [SerializeField] int maxCoincsPerCollect = 75;

    [Header("Booster")]

    [SerializeField] GameObject boosterButton;
    [SerializeField] int boostDuration = 3;

    private bool hasEquippedHat = false;

    private Coroutine ScaleCoroutine;
    private Rigidbody2D rb;

    private bool pushLeft = false;
    private bool pushRight = false;

    private int score = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();


    }
    void Start()
    {
        if (boosterButton)
        {
            boosterButton.SetActive(false);
            if (PlayerPrefs.GetInt("BoosterStore") == 1)
            {
                boosterButton.SetActive(true);
            }
        }
        score = 0;

        string balloonSkin = PlayerPrefs.GetString("BalloonSkin");
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (balloonSkin != "" && balloonSkin != spriteRenderer.sprite.name)
        {
            Sprite skin = Resources.Load<Sprite>("Balloons/"+balloonSkin);
            if (skin)
            {
                spriteRenderer.sprite = skin;
            }
        }

        string hatSkin = PlayerPrefs.GetString("HatSkin");
        if (hatSkin != "" && hatSkin != HatSprite.name)
        {
            Sprite skin = Resources.Load<Sprite>("Hats/"+hatSkin);
            if (skin)
            {
                HatSprite = skin;
            }
        }
    }
    void FixedUpdate()
    {
        if (!rb)
            return;

        if (pushLeft)
            rb.AddRelativeForceX(-sidePushForce, ForceMode2D.Force);
        if (pushRight)
            rb.AddRelativeForceX(sidePushForce, ForceMode2D.Force);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Obstacle")
        {
            if (!hasEquippedHat)
            {
                if (levelManager)
                {
                    levelManager.StopGameByLose();
                    ScaleBallonTo(Vector3.zero);
                    MusicManager.singleton?.PlayLoseSound();
                }
                else
                {
                    Debug.Log($"Please assign levelManager on {this.gameObject.name}");
                }
            }
            else
            {
                if (HatSocket)
                {
                    HatSocket.sprite = null;
                    hasEquippedHat = false;
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hat")
        {
            hasEquippedHat = true;
            if (HatSocket != null && HatSprite != null)
            {
                HatSocket.sprite = HatSprite;
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Coin")
        {
            int coinsToCollect = Random.Range(minCoincsPerCollect, maxCoincsPerCollect);
            CoinManager.singleton.AddCoins(coinsToCollect);
            MusicManager.singleton?.PlayCoinPickUpSound();
            score += coinsToCollect;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Finish")
        {
            if (levelManager)
            {
                levelManager.StopGameByWin();
                MusicManager.singleton?.PlayWinSound();
            }
        }
    }

    private void ScaleBallonTo(Vector3 endScale_, float duration_ = 0.3f)
    {
        if (ScaleCoroutine != null)
        {
            StopCoroutine(ScaleCoroutine);
        }
        ScaleCoroutine = StartCoroutine(ScaleTo(endScale_, duration_));
    }



    private IEnumerator ScaleTo(Vector3 endScale_, float duration_ = 0.3f)
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = endScale_;

        float elapsed = 0f;

        while (elapsed < duration_)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration_);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;


    }
    public void UseBoost()
    {
        StartCoroutine(Boost());
    }
    private IEnumerator Boost()
    {
        PlayerPrefs.SetInt("BoosterStore", 0);
        boosterButton.SetActive(false);

        Vector3 startScale = transform.localScale;
        ScaleBallonTo(new Vector3(0.5f, 0.5f, 0.5f));
        yield return new WaitForSeconds(boostDuration);

        if (rb.simulated)
            ScaleBallonTo(startScale);

    }


    public void SetPushLeft(bool push)
    {
        pushLeft = push;
    }
    public void SetPushRight(bool push)
    {
        pushRight = push;
    }

    public int GetScore()
    {
        return score;
    }
}
