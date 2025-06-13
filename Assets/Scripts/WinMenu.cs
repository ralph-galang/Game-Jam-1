using UnityEngine;

public class WinMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.WIN, this.DisplayWinScreen);
        EventBroadcaster.Instance.AddObserver(EventNames.GAME_RESTART, this.CloseWinScreen);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void onDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.WIN);
        EventBroadcaster.Instance.RemoveObserver(EventNames.GAME_RESTART);
    }

    void DisplayWinScreen()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    void CloseWinScreen()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
