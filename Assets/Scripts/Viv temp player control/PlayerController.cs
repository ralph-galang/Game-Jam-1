using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    void Start()
    {
        
    }


    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W)) { 
            this.gameObject.transform.position +=
                this.gameObject.transform.forward * playerData.getWalkSpd() * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.position +=
                this.gameObject.transform.forward * -playerData.getWalkSpd() * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.Rotate(0f, -playerData.getRotateSpd() * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.Rotate(0f, playerData.getRotateSpd() * Time.deltaTime, 0f);
        }

    }
}
