using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool isPaused = false;
    private bool isHeadHit = false;
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.HEAD_HIT, this.HeadHit);
    }

    void onDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.HEAD_HIT);
    }
    void HeadHit()
    {
        isHeadHit = true;
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
        Time.timeScale -= 0.005f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeadHit && Time.timeScale >= 0.75f) SlowMotion(); 

        if (Input.GetKeyDown(KeyCode.Escape) && !isHeadHit)
        {
            TogglePause();
        }
    }
}
