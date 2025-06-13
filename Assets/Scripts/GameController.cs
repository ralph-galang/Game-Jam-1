using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject StartScreenMenu;
    [SerializeField] GameObject CreditsScreen;
    bool isStartScreenOpen = true;
    [SerializeField] float slowMo=0.9f;
    [SerializeField] bool enableSlowMo = false;
    public GameObjectPool pool;
    private bool isPaused = false;
    private bool isStarted = false;
    private bool isWin = false;
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.WIN, this.Win);
        EventBroadcaster.Instance.AddObserver(EventNames.GAME_RESTART, this.SetUp);
    }

    void onDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.WIN);
        EventBroadcaster.Instance.RemoveObserver(EventNames.GAME_RESTART);
    }

    public void CloseStartScreenMenu()
    {
        StartScreenMenu.SetActive(false);
        CloseCreditsScreen();
        isStartScreenOpen = false;
    }
    public void OpenStartScreenMenu()
    {
        StartScreenMenu.SetActive(true);
        isStartScreenOpen = true;
    }

    public void CloseCreditsScreen()
    {
        CreditsScreen.SetActive(false);
    }
    public void OpenCreditsScreen()
    {
        CreditsScreen.SetActive(true);
    }

    void SetUp()
    {
        isWin = false;
        isStarted = false;
    }

    void Win()
    {
        isWin = true;
        score++;
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        PauseGame();
    }

    private void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    void SlowMotion()
    {
        if (Time.timeScale >= slowMo) Time.timeScale -= 0.05f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWin && enableSlowMo) SlowMotion();

        if (!isStartScreenOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isWin)
            {
                TogglePause();
                Parameters pauseVal = new Parameters();
                pauseVal.PutExtra("PauseVal", isPaused);
                EventBroadcaster.Instance.PostEvent(EventNames.GAME_PAUSE, pauseVal);
            }

            if (Input.GetKeyDown(KeyCode.Tab) && !isStarted)
            {
                isStarted = true;
                EventBroadcaster.Instance.PostEvent(EventNames.GAME_START);
                //Debug.Log("Game has Started");
                EventBroadcaster.Instance.PostEvent("startSpawn");
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                EventBroadcaster.Instance.PostEvent(EventNames.GAME_RESTART);
                //Debug.Log("Clear clutter");
                EventBroadcaster.Instance.PostEvent("ClearPool");
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                EventBroadcaster.Instance.PostEvent(EventNames.GAME_RESTART);
                EventBroadcaster.Instance.PostEvent("ClearPool");
                isStartScreenOpen = true;
                OpenStartScreenMenu();
            }
        }
        
    }
}
