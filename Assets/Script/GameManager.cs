

using UnityEngine;
using TMPro;



public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject pausePanel;

    public enum GameState
    {
        Playing, Pause, Lose, Won, MainMenu
    };

    public GameState CurrentState { get; private set; }


    [Header("UI GamePanel")]
    [SerializeField] private TMP_Text gameScoreText;
    [SerializeField] private TMP_Text gameCoinText;

    [Header("UI MenuPanel ")]
    [SerializeField] private TMP_Text mainTotalCoinText;

    [Header("WinPanel")]
    [SerializeField] private TMP_Text winScoreText;
    [SerializeField] private TMP_Text winCoinText;
    [SerializeField] private TMP_Text winTotalCoinText;


    private int currentScore;
    private int currentCoin;
    private int totalCoin;

    public const string SAVE_KEY = "TOTAL COINS";
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }




    void Start()
    {
        LoadCoin();
        ShowMainMenu();
        UpdateUI();
    }

    public void ShowMainMenu()
    {

        CurrentState = GameState.MainMenu;
        Time.timeScale = 1f;

        SetPanel(main: true, win: false, game: false, lose: false, pause: false);


    }

    public void StartGame()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
        currentScore = 0;
        currentCoin = 0;
        SetPanel(main: false, win: false, game: true, lose: false, pause: false);
        UpdateUI();

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadLevel(0);
        }
    }

    public void Pause()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Pause;
        Time.timeScale = 0f;
        SetPanel(main: false, win: false, game: true, lose: false, pause: true);

    }
    public void Resume()
    {
        if (CurrentState != GameState.Pause) return;
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
        SetPanel(main: false, win: false, game: true, lose: false, pause: false);

    }

    public void Win()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Won;
        Time.timeScale = 0f;
        SetPanel(main: false, win: true, game: false, lose: false, pause: false);
        UpdateUI();


    }


    public void NextLevel()
    {
        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
        SetPanel(main: false, win: false, game: true, lose: false, pause: false);

        if (LevelManager.Instance != null) LevelManager.Instance.LoadNextLevel();

    }

    public void Restart()
    {
        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
        currentCoin = 0;
        currentScore = 0;

        SetPanel(main: false, win: false, game: true, lose: false, pause: false);
        UpdateUI();
        if (PlayerMovement.Instance != null) PlayerMovement.Instance.Revive();




        if (LevelManager.Instance != null) LevelManager.Instance.RestartLevel();

    }


    public void Lose()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Lose;
        Time.timeScale = 0f;
        SetPanel(main: false, win: false, game: false, lose: true, pause: false);

    }

    public void Exit()
    {
        Application.Quit();
    }




    public void SetPanel(bool main, bool win, bool game, bool lose, bool pause)
    {
        if (mainPanel != null) mainPanel.SetActive(main);
        if (winPanel != null) winPanel.SetActive(win);
        if (gamePanel != null) gamePanel.SetActive(game);
        if (losePanel != null) losePanel.SetActive(lose);
        if (pausePanel != null) pausePanel.SetActive(pause);
    }


    public void AddCoin(int coinAmount, int scoreAmount)
    {
        currentCoin += coinAmount;
        totalCoin += coinAmount;
        currentScore += scoreAmount;
        SaveCoin();
        UpdateUI();

    }
    public void AddScore(int scoreAmount)
    {
        currentScore += scoreAmount;
        UpdateUI();
    }
    public void LoadCoin()
    {
        totalCoin = PlayerPrefs.GetInt(SAVE_KEY, 0);
    }

    public void UpdateUI()
    {
        if (gameCoinText != null)
            gameCoinText.text = "Coin : " + currentCoin;

        if (gameScoreText != null)
            gameScoreText.text = " Score : " + currentScore;

        if (mainTotalCoinText != null)
            mainTotalCoinText.text = " Total Coin :" + totalCoin;

        if (winCoinText != null)
            winCoinText.text = "Coin Collected  : " + currentCoin;

        if (winScoreText != null)
            winScoreText.text = " Score :" + currentScore;

        if (winTotalCoinText != null)
            winTotalCoinText.text = "Total Coin: " + totalCoin;




    }

    public void SaveCoin()
    {
        PlayerPrefs.SetInt(SAVE_KEY, totalCoin);
        PlayerPrefs.Save();

    }



}
