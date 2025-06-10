using UnityEngine;

public class TsinelasController : MonoBehaviour
{
    [SerializeField] GameObject temp;
    
    [SerializeField] float explosionForce = 10.0f;
    [SerializeField] float explosionRadius = 5.0f;
    [SerializeField] float upwardMod = 1.0f;
    //TEMP ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    [SerializeField] TsinelasData tsinelasData;
    [SerializeField] GameObject rotationController;
    [SerializeField] GameObject tsinelasModel;

    [SerializeField] Rigidbody rb;
    bool isSteering = false;
    Quaternion steerTarget;

    bool isRelaxed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.HEAD_HIT, this.Relax);
        rb.AddForce(this.gameObject.transform.forward * tsinelasData.forwardSpeed);
        rb.useGravity = false;
    }

    void onDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.HEAD_HIT);
    }

    void Relax()
    {
        isRelaxed = true;
        EnableGravity();
    }

    void EnableGravity()
    {
        rb.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        isSteering = false;
        if (Input.GetKey(KeyCode.W))
        {
            isSteering = true;
            this.gameObject.transform.position += this.gameObject.transform.up * tsinelasData.speed * Time.deltaTime;
            steerTarget = Quaternion.Euler(new Vector3(-30.0f, 0.0f, 0.0f));
            rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, steerTarget, tsinelasData.steeringSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            isSteering = true;
            this.gameObject.transform.position -= this.gameObject.transform.up * tsinelasData.speed * Time.deltaTime;
            steerTarget = Quaternion.Euler(new Vector3(30.0f, 0.0f, 0.0f));
            rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, steerTarget, tsinelasData.steeringSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            isSteering = true;
            this.gameObject.transform.position -= this.gameObject.transform.right * tsinelasData.speed * Time.deltaTime;
            steerTarget = Quaternion.Euler(new Vector3(0.0f, 0.0f, 30.0f));
            rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, steerTarget, tsinelasData.steeringSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            isSteering = true;
            this.gameObject.transform.position += this.gameObject.transform.right * tsinelasData.speed * Time.deltaTime;
            steerTarget = Quaternion.Euler(new Vector3(0.0f, 0.0f, -30.0f));
            rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, steerTarget, tsinelasData.steeringSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(this.gameObject.transform.forward * tsinelasData.dashSpeed);
        }

        if (!isSteering) rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, Quaternion.identity, tsinelasData.courseCorrect * Time.deltaTime);

        if (!isRelaxed)
        {
            this.gameObject.transform.position += this.gameObject.transform.forward * tsinelasData.forwardSpeed * Time.deltaTime;
            tsinelasModel.transform.Rotate(0.0f, 0.0f, tsinelasData.spinSpeed * Time.deltaTime, Space.Self);
        }

        if (isRelaxed)
        {
            rb.AddExplosionForce(explosionForce, temp.transform.position, explosionRadius, upwardMod);
        }
    }
}
