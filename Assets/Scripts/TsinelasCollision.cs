using UnityEngine;

public class TsinelasCollision : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.GAME_RESTART, this.SetUp);
    }

    void onDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.GAME_RESTART);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetUp()
    {
        this.enabled= true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Anak")
        {
            EventBroadcaster.Instance.PostEvent(EventNames.LOSE);
        }
        else
        {
            EventBroadcaster.Instance.PostEvent(EventNames.WIN);
        }
        this.enabled = false;
    }
}


