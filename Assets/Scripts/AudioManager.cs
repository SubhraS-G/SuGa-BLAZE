using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Music")]
    public AudioClip backgroundMusic;

    [Header("SFX")]
    public AudioClip jumpSFX;
    public AudioClip coinSFX;
    public AudioClip deathSFX;
    public AudioClip slideSFX;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Create audio sources via code
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0.5f;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.volume = 1f;
    }

    void Start()
    {
        Debug.Log("AudioManager started!");
        if (backgroundMusic != null)
        {
            Debug.Log("Music clip found!");
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
        else
        {
            Debug.Log("No music clip assigned!");
        }
    }

    public void PlayJump()
    {
        if (jumpSFX != null)
            sfxSource.PlayOneShot(jumpSFX);
    }

    public void PlayCoin()
    {
        if (coinSFX != null)
            sfxSource.PlayOneShot(coinSFX, 0.7f);
    }

    public void PlayDeath()
    {
        if (deathSFX != null)
            sfxSource.PlayOneShot(deathSFX);
    }

    public void PlaySlide()
    {
        if (slideSFX != null)
            sfxSource.PlayOneShot(slideSFX, 0.6f);
    }
}