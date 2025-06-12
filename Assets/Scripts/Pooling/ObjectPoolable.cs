using UnityEngine;
using System.Collections;

public class ObjectPoolable : APoolable
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            StartCoroutine(ShrinkOverTime(1f));
        }
    }

    IEnumerator ShrinkOverTime(float duration)
    {
        Vector3 start = transform.localScale;
        Vector3 end = Vector3.zero;
        float timer = 0f;

        while (timer < duration)
        {
            transform.localScale = Vector3.Lerp(start, end, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;
        this.poolRef.ReleasePoolable(this);
    }


    public override void Initialize()
    {

    } 

    public override void Release()
    {

    }

    public override void OnActivate()
    {

    }
}
