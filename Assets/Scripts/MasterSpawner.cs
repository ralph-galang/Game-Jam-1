using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MasterSpawner : MonoBehaviour
{
    [SerializeField] SpawnerController[] spawners;
    [SerializeField] Transform player;
    [SerializeField] int maxCluster;
    [SerializeField] int currentIndex = 0;
    [SerializeField] float maxHorizontalDistance = 5f;
    [SerializeField] float maxVerticallDistance = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver("startSpawn", this.startSpawn);
        EventBroadcaster.Instance.AddObserver("finishSpawn", this.nextSpawner);
        EventBroadcaster.Instance.AddObserver(EventNames.GAME_RESTART, this.disableSpawn);
        EventBroadcaster.Instance.AddObserver(EventNames.GAME_RESTART, this.ResetCurrentIndex);
    }
    void Start()
    {
        int[] splitCluster = randomSplit(spawners.Length, maxCluster);

        for (int i = 0; i < spawners.Length; i++)
        {

            spawners[i].setSpawnerCount(splitCluster[i]);
        }

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].Disable();
        }
    }

    private void ResetCurrentIndex()
    {
        currentIndex = 0;
    }

    private void disableSpawn()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].Disable();
        }
    }

    private void startSpawn()
    {
        StartCoroutine(startFirstSpawner(1));
    }

    IEnumerator startFirstSpawner(float timer)
    {
        yield return new WaitForSeconds(timer);
        spawners[currentIndex].Enable();
        EventBroadcaster.Instance.PostEvent("spawnObject");
    }

    private void Update()
    {
        Vector3 setPosition = player.position + player.forward * maxHorizontalDistance + Vector3.up * maxVerticallDistance;
        if (currentIndex < spawners.Length)
        {
            spawners[currentIndex].transform.position = setPosition;
        }
    }
    void nextSpawner()
    {
        currentIndex++;

        if (currentIndex < spawners.Length)
        {
            spawners[currentIndex].Enable();
            EventBroadcaster.Instance.PostEvent("spawnObject");

        }
    }
    private int[] randomSplit(int spawnerAmount, int clusterAmount)
    {
        int[] result = new int[spawnerAmount];
        int remainder = clusterAmount;

        for (int i = 0; i < spawnerAmount - 1; i++)
        {
            int value = Random.Range(0, remainder + 1);
            result[i] = value;
            remainder = remainder - value;
        }

        result[spawnerAmount - 1] = remainder;

        return result;
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver("finishSpawn");
    }
}
