using UnityEngine;
using System.Collections;

public class PoolManager : MonoBehaviour
{
    [SerializeField] GameObjectPool pool;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver("ClearPool", this.ClearPool);
    }

    private void ClearPool()
    {
        pool.RequestPoolable();
    }

    void Start()
    {
        pool.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        //if (ticks < spawnInterval)
        //{
        //    ticks += Time.deltaTime;
        //}
        //else if (pool.HasObjectAvailable(1))
        //{
        //    this.ticks = 0;
        //    pool.RequestPoolable();

        //    this.spawnInterval = Random.Range(0.5f, 1.0f);
        //}
    }

    void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver("ClearPool");
    }
}
