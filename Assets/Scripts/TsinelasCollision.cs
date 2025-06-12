using UnityEngine;

public class TsinelasCollision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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


