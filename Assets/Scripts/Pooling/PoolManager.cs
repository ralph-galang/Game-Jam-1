using UnityEngine;
using System.Collections;

public class PoolManager : MonoBehaviour
{
    [SerializeField] GameObjectPool pool;

    void Start()
    {
        pool.Initialize();
    }
}
