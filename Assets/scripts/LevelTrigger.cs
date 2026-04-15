using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelTrigger : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public int nextLevelIndex;
    public GameObject canvas;
    public Text timerText;
    public Text finalTimeText;

    [Header("Pause UI")]
    public Button pauseButton;
    public GameObject pauseMenu;
    public Button resumeButton;

    private float startTime;
    private float elapsedTime;
    private bool isTimerRunning = false;
    private bool isGamePaused = false;
    private bool isLevelCompleted = false;
    private string finalTimeString;

    void Start()
    {
        // «· √ﬂœ „‰ ≈Œ›«¡ «·ﬂ«‰›” ÊÊ«ÃÂ… «·≈Ìﬁ«› ›Ì «·»œ«Ì…
        if (canvas != null)
        {
            canvas.SetActive(false);
        }

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        // ≈⁄œ«œ √“—«— «·≈Ìﬁ«› Ê«·«” ∆‰«›
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(PauseGame);
        }

        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }

        // »œ¡ «· «Ì„—
        StartTimer();
    }

    void Update()
    {
        //  ÕœÌÀ «· «Ì„— ≈–« ﬂ«‰ Ì⁄„·
        if (isTimerRunning && !isGamePaused && !isLevelCompleted)
        {
            elapsedTime = Time.time - startTime;
            UpdateTimerDisplay();
        }

        // ≈÷«›… ≈„ﬂ«‰Ì… «·≈Ìﬁ«› «·„ƒﬁ  »«” Œœ«„ “— ESC
        if (Input.GetKeyDown(KeyCode.Escape) && !isLevelCompleted)
        {
            if (isGamePaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void StartTimer()
    {
        startTime = Time.time;
        isTimerRunning = true;
        elapsedTime = 0f;
        isLevelCompleted = false;

        if (timerText != null)
        {
            timerText.text = "00:00.00";
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = (int)(elapsedTime / 60);
            int seconds = (int)(elapsedTime % 60);
            int milliseconds = (int)((elapsedTime * 100) % 100);

            timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //  Õﬁﬁ „‰ √‰ «·„ ’«œ„ ÂÊ «··«⁄»
        if (collision.CompareTag("Player") || collision.gameObject == player)
        {
            Debug.Log("Player entered the trigger!");

            // ≈Ìﬁ«› «· «Ì„—
            isTimerRunning = false;
            isLevelCompleted = true;

            // Õ›Ÿ «·Êﬁ  «·‰Â«∆Ì
            finalTimeString = timerText != null ? timerText.text : "00:00.00";

            // ≈ŸÂ«— «·ﬂ«‰›” Ê⁄—÷ «·Êﬁ  «·‰Â«∆Ì
            if (canvas != null)
            {
                canvas.SetActive(true);

                // ⁄—÷ «·Êﬁ  «·‰Â«∆Ì
                if (finalTimeText != null)
                {
                    finalTimeText.text = finalTimeString;
                }
            }

            // ≈Ìﬁ«› Õ—ﬂ… «··«⁄»
           

            // ≈Œ›«¡ “— «·≈Ìﬁ«› «·„ƒﬁ 
            if (pauseButton != null)
            {
                pauseButton.gameObject.SetActive(false);
            }

            // ≈Œ›«¡ ﬁ«∆„… «·≈Ìﬁ«› «·„ƒﬁ  ≈–« ﬂ«‰  Ÿ«Â—…
            if (pauseMenu != null && pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
            }

            // ≈Ìﬁ«› «·Êﬁ  ›Ì «··⁄»…
            Time.timeScale = 0f;
        }
    }

    public void NextLevel()
    {
        // ≈⁄«œ… «·Êﬁ  «·ÿ»Ì⁄Ì ﬁ»·  Õ„Ì· «·„‘Âœ «· «·Ì
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextLevelIndex);
    }

    public void BackToMain()
    {
        // ≈⁄«œ… «·Êﬁ  «·ÿ»Ì⁄Ì ﬁ»·  Õ„Ì· «·„‘Âœ «·—∆Ì”Ì
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        if (isLevelCompleted) return; // ·« Ì„ﬂ‰ ≈Ìﬁ«› «··⁄»… ≈–« «‰ ÂÏ «·„” ÊÏ

        isGamePaused = true;

        // ≈Ìﬁ«› «··⁄»… „ƒﬁ «
        Time.timeScale = 0f;

        // ≈Ìﬁ«› «· «Ì„— „ƒﬁ «
        isTimerRunning = false;

        // ≈ŸÂ«— Ê«ÃÂ… «·≈Ìﬁ«› «·„ƒﬁ 
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }

        Debug.Log("«··⁄»… „ Êﬁ›… „ƒﬁ «");
    }

    public void ResumeGame()
    {
        isGamePaused = false;

        // «” ∆‰«› «··⁄»…
        Time.timeScale = 1f;

        // «” ∆‰«› «· «Ì„— ≈–« ·„ Ìﬂ‰ «··«⁄» ﬁœ «‰ ÂÏ „‰ «·„” ÊÏ
        if (!isLevelCompleted)
        {
            isTimerRunning = true;
            startTime = Time.time - elapsedTime;
        }

        // ≈Œ›«¡ Ê«ÃÂ… «·≈Ìﬁ«› «·„ƒﬁ 
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        Debug.Log("«” ∆‰«› «··⁄»…");
    }

    // œ«·… ·≈⁄«œ… «· ‘€Ì· ≈–« ﬂ‰   Õ «ÃÂ«
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}