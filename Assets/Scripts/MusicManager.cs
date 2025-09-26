using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip Music;
    [SerializeField] AudioClip WinSound;
    [SerializeField] AudioClip LoseSound;
    [SerializeField] AudioClip CoinPickUpSound;
    AudioSource MusicSource;
    AudioSource PlayerSource;
    public static MusicManager singleton;

    void Awake()
{
    if (singleton != null && singleton != this)
    {
        Destroy(gameObject);
        return;
    }

    singleton = this;
    DontDestroyOnLoad(gameObject);
}

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        MusicSource = GetComponent<AudioSource>();
        if (MusicSource && Music)
        {
            MusicSource.loop = true;
            MusicSource.clip = Music;
            MusicSource.Play();
        }
        singleton = this;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var Player = GameObject.FindGameObjectWithTag("Player");
        if(Player)
        PlayerSource = Player.GetComponent<AudioSource>();
    }

    public void PlayWinSound()
    {
        if (!WinSound || !PlayerSource)
            return;

        PlayerSource.clip = WinSound;
        PlayerSource.Play();
    }
    public void PlayLoseSound()
    {
        if (!LoseSound || !PlayerSource)
            return;

        PlayerSource.clip = LoseSound;
        PlayerSource.Play();
    }
    public void PlayCoinPickUpSound()
    {
        if (!CoinPickUpSound || !PlayerSource)
            return;

        PlayerSource.clip = CoinPickUpSound;
        PlayerSource.Play();
    }
}
