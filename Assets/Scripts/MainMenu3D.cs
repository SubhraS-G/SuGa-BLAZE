using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu3D : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI titleText;

    void Start()
    {
        int best = PlayerPrefs.GetInt("BestScore3D", 0);
        if (bestScoreText != null)
            bestScoreText.text = "Best Score: " + best;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}