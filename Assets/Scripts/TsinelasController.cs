using UnityEngine;

public class TsinelasController : MonoBehaviour
{
    [SerializeField] GameObject explosionEpicenter;
    [SerializeField] float explosionForce = 10.0f;
    [SerializeField] float explosionRadius = 5.0f;
    [SerializeField] float upwardMod = 1.0f;
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
        EventBroadcaster.Instance.AddObserver(EventNames.WIN, this.Relax);
        EventBroadcaster.Instance.AddObserver(EventNames.LOSE, this.Relax);
        rb.useGravity = false;
    }

    void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.WIN);
        EventBroadcaster.Instance.RemoveObserver(EventNames.LOSE);
    }
    void ExplosionForce()
    {
        // Tsinelas bounces off of the Anak
        rb.AddExplosionForce(explosionForce, explosionEpicenter.transform.position, explosionRadius, upwardMod);
    }

    void Relax()
    {
        isRelaxed = true;
        EnableGravity();
        ExplosionForce();
    }

    void EnableGravity()
    {
        rb.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        isSteering = false;

        if (!isRelaxed) // Animation and Movement
        {
            if (Input.GetKey(KeyCode.W))
            {
                isSteering = true;
                this.gameObject.transform.position += this.gameObject.transform.up * tsinelasData.speed * Time.deltaTime;
                steerTarget = Quaternion.Euler(new Vector3(-tsinelasData.angleOfSteering, 0.0f, 0.0f));
                rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, steerTarget, tsinelasData.steeringSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                isSteering = true;
                this.gameObject.transform.position -= this.gameObject.transform.up * tsinelasData.speed * Time.deltaTime;
                steerTarget = Quaternion.Euler(new Vector3(tsinelasData.angleOfSteering, 0.0f, 0.0f));
                rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, steerTarget, tsinelasData.steeringSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                isSteering = true;
                this.gameObject.transform.position -= this.gameObject.transform.right * tsinelasData.speed * Time.deltaTime;
                steerTarget = Quaternion.Euler(new Vector3(0.0f, 0.0f, tsinelasData.angleOfSteering));
                rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, steerTarget, tsinelasData.steeringSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                isSteering = true;
                this.gameObject.transform.position += this.gameObject.transform.right * tsinelasData.speed * Time.deltaTime;
                steerTarget = Quaternion.Euler(new Vector3(0.0f, 0.0f, -tsinelasData.angleOfSteering));
                rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, steerTarget, tsinelasData.steeringSpeed * Time.deltaTime);
            }

            // Move Tsinelas Forward
            this.gameObject.transform.position += this.gameObject.transform.forward * tsinelasData.forwardSpeed * Time.deltaTime;

            // Spin Tsinelas Model
            tsinelasModel.transform.Rotate(0.0f, 0.0f, tsinelasData.spinSpeed * Time.deltaTime, Space.Self);

            // When the player is not steering, correct the orientation of the spin of the tsinelas
            if (!isSteering) rotationController.transform.localRotation = Quaternion.Slerp(rotationController.transform.localRotation, Quaternion.identity, tsinelasData.courseCorrect * Time.deltaTime);
        }
    }
}
