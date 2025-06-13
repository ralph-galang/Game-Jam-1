using UnityEngine;

public class LoseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.LOSE, this.DisplayLoseScreen);
        EventBroadcaster.Instance.AddObserver(EventNames.GAME_RESTART, this.CloseLoseScreen);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void onDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.LOSE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.GAME_RESTART);
    }

    void DisplayLoseScreen()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    void CloseLoseScreen()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
