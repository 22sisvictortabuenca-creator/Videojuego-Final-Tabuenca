using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject losePanel; 
    public GameObject winPanel; 

    [Header("UI Text (Arrastra textos de tipo TextMeshPro)")]
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI livesText; 

    private int score = 0;

    void Awake()
    {
        // Al recargar la escena, nos aseguramos de que la instancia sea la más reciente
        instance = this;
    }

    void Start()
    {
        if (losePanel != null) losePanel.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);
        UpdateScoreUI();
    }

    void Update()
    {
        // Activar el reinicio con una tecla (por ejemplo, la R) si el panel de derrota está activo
        if (losePanel != null && losePanel.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Frutas: " + score;
        }
    }

    public void UpdateLivesUI(int lives)
    {
        if (livesText != null)
        {
            livesText.text = "Vidas: " + lives;
        }
        else
        {
            Debug.LogWarning("¡ATENCIÓN! No has arrastrado el texto de las Vidas al hueco de 'Lives Text' en el GameManager.");
        }
    }

    public void ShowGameOver()
    {
        if (losePanel != null) losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowVictory()
    {
        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}