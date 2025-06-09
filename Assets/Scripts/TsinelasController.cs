using UnityEngine;

public class TsinelasController : MonoBehaviour
{
    [SerializeField] TsinelasData tsinelasData;
    [SerializeField] GameObject rotationController;
    [SerializeField] GameObject tsinelasModel;
    
    bool isSteering = false;
    Quaternion steerTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)){
            isSteering=true;
            this.gameObject.transform.position+=this.gameObject.transform.up * tsinelasData.speed*Time.deltaTime;
            steerTarget = Quaternion.Euler(new Vector3(-30.0f,0.0f,0.0f));
            rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, steerTarget, tsinelasData.steeringSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.S)){
            isSteering=true;
            this.gameObject.transform.position-=this.gameObject.transform.up * tsinelasData.speed*Time.deltaTime;
            steerTarget = Quaternion.Euler(new Vector3(30.0f,0.0f,0.0f));
            rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, steerTarget, tsinelasData.steeringSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A)) {
            isSteering=true;
            this.gameObject.transform.position-=this.gameObject.transform.right * tsinelasData.speed*Time.deltaTime;
            steerTarget = Quaternion.Euler(new Vector3(0.0f,0.0f,30.0f));
            rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, steerTarget, tsinelasData.steeringSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D)) {
            isSteering=true;
            this.gameObject.transform.position+=this.gameObject.transform.right * tsinelasData.speed*Time.deltaTime;
            steerTarget = Quaternion.Euler(new Vector3(0.0f,0.0f,-30.0f));
            rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, steerTarget, tsinelasData.steeringSpeed * Time.deltaTime);
        }
        
        if (!isSteering) rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, Quaternion.identity, tsinelasData.courseCorrect * Time.deltaTime);
            
        this.gameObject.transform.position += this.gameObject.transform.forward * Time.deltaTime * tsinelasData.forwardSpeed;

        tsinelasModel.transform.Rotate(0.0f,0.0f,tsinelasData.spinSpeed * Time.deltaTime, Space.Self);
        isSteering=false;
    }
}
