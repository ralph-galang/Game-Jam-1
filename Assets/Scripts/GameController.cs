using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] float slowMo=0.9f;
    [SerializeField] bool enableSlowMo = false;
    private bool isPaused = false;
    private bool isWin = false;
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.WIN, this.Win);
    }

    void onDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.WIN);
    }

    void onCollisionEnter(Collider other)
    {
        if (other.tag == "Anak") Debug.Log("Collision Detected");//EventBroadcaster.Instance.PostEvent(EventNames.WIN);
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

        if (Input.GetKeyDown(KeyCode.Escape) && !isWin)
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spawn 1 clutter");
            EventBroadcaster.Instance.PostEvent("spawnObject");
        }
    }
}
