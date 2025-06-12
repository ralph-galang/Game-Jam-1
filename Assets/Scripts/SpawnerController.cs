using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody[] objType;
    [SerializeField] private int spawnCount = 0;
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private float rotateSpd = 10f;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver("spawnObject", this.spawnObject);

        Enable();
    }

    void spawnObject()
    {
        if (this.isActiveAndEnabled)
        {
            StartCoroutine(SpawnItems(1f));
        }
    }

    IEnumerator SpawnItems(float interval)
    {
        Debug.Log("start spawning");
        for (int i = 0; i < spawnCount; i++)
        {
            int randObj = Random.Range(0, objType.Length);
            float shootSpeed = Random.Range(15f, 30f);

            Rigidbody spawnObject = Instantiate(objType[randObj], transform.position, Quaternion.identity);
            spawnObject.gameObject.SetActive(true);

            Vector3 direction = (player.position - transform.position).normalized;
            direction += new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.1f, 0.2f), Random.Range(-0.3f, 0.3f));
            spawnObject.AddForce(direction * 10f, ForceMode.Impulse);

            spawnObject.angularVelocity = Random.insideUnitSphere * rotateSpd;

            yield return new WaitForSeconds(interval);
        }
        Disable();
        EventBroadcaster.Instance.PostEvent("finishSpawn");
    }

    public void setSpawnerCount(int countVal){ spawnCount = countVal; }

    public void Disable() { this.gameObject.SetActive(false); }
    public void Enable() { this.gameObject.SetActive(true); }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver("spawnObject");
    }
}