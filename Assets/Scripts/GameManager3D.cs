using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager3D : MonoBehaviour
{
    public static GameManager3D instance;

    [Header("UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI finalCoinsText;

    private bool isGameOver = false;
    private float score = 0f;
    private int coins = 0;
    private PlayerController3D player;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        player = FindAnyObjectByType<PlayerController3D>();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;

        score += Time.deltaTime * 10f;

        if (scoreText != null)
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        if (coinsText != null)
            coinsText.text = "Coins: " + coins;
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (AudioManager.instance != null)
            AudioManager.instance.PlayDeath();

        PlayerController3D pc =
            FindAnyObjectByType<PlayerController3D>();
        if (pc != null) pc.enabled = false;

        if (finalScoreText != null)
            finalScoreText.text =
                "Score: " + Mathf.FloorToInt(score);

        if (finalCoinsText != null)
            finalCoinsText.text = "Coins: " + coins;

        int best = PlayerPrefs.GetInt("BestScore3D", 0);
        if (Mathf.FloorToInt(score) > best)
        {
            PlayerPrefs.SetInt(
                "BestScore3D", Mathf.FloorToInt(score));
            PlayerPrefs.Save();
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name);
    }
}