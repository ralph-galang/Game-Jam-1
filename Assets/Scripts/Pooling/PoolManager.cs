using UnityEngine;
using System.Collections;

public class PoolManager : MonoBehaviour
{
    [SerializeField] GameObjectPool pool;

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
}
