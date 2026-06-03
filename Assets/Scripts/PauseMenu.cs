using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    [Header("UI")]
    public GameObject pausePanel;

    private bool isPaused = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        // Keyboard pause — Escape key
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
            Pause();
        else
            Resume();
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu3D");
    }
}