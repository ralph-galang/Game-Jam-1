using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.GAME_PAUSE, this.PauseGame);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void onDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.GAME_PAUSE);
    }

    void PauseGame(Parameters param)
    {
        isPaused = param.GetBoolExtra("PauseVal", false);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isPaused);
        }
    }
}
