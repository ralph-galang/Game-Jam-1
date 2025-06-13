using UnityEngine;

public class TsinelasController : MonoBehaviour
{
    AudioSource flyingSFX;
    bool isPlayingFlyingSFX;
    [SerializeField] GameObject explosionEpicenter;
    [SerializeField] float explosionForce = 10.0f;
    [SerializeField] float explosionRadius = 5.0f;
    [SerializeField] float upwardMod = 1.0f;
    [SerializeField] TsinelasData tsinelasData;
    [SerializeField] GameObject rotationController;
    [SerializeField] GameObject tsinelasModel;
    [SerializeField] GameObject startingPoint;

    [SerializeField] Rigidbody rb;
    bool isSteering = false;
    Quaternion steerTarget;

    bool isRelaxed = false;
    bool isPaused = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.WIN, this.Relax);
        EventBroadcaster.Instance.AddObserver(EventNames.LOSE, this.Relax);
        EventBroadcaster.Instance.AddObserver(EventNames.GAME_PAUSE, this.Pause);
        EventBroadcaster.Instance.AddObserver(EventNames.GAME_START, this.GameStart);
        EventBroadcaster.Instance.AddObserver(EventNames.GAME_RESTART, this.SetUp);
        SetUp();
    }

    void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.WIN);
        EventBroadcaster.Instance.RemoveObserver(EventNames.LOSE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.GAME_START);
        EventBroadcaster.Instance.RemoveObserver(EventNames.GAME_RESTART);
    }

    void SetUp()
    {
        DisableGravity();
        EnableKinematic();
        isRelaxed = false;
        isPaused = true;
        this.gameObject.transform.position = startingPoint.transform.position;
        tsinelasModel.transform.position = startingPoint.transform.position;
        tsinelasModel.transform.localRotation = startingPoint.transform.localRotation;
        rotationController.transform.localRotation = Quaternion.identity;
        flyingSFX = GetComponent<AudioSource>();
        SetAudioVolume(0.0f);
        isPlayingFlyingSFX = false;
    }

    void GameStart()
    {
        DisableGravity();
        EnableKinematic();
        isPaused = false;
        SetAudioVolume(1.0f);
        isPlayingFlyingSFX = true;
    }

    void ExplosionForce()
    {
        // Tsinelas bounces off of the Anak
        rb.AddExplosionForce(explosionForce, explosionEpicenter.transform.position, explosionRadius, upwardMod);
    }

    void Relax()
    {
        isRelaxed = true;
        isPlayingFlyingSFX = false;
        DisableKinematic();
        EnableGravity();
        ExplosionForce();
        SetAudioVolume(0.0f);
    }

    void DisableKinematic()
    {
        rb.isKinematic = false;
    }

    void EnableKinematic()
    {
        rb.isKinematic = true;
    }

    void DisableGravity()
    {
        rb.useGravity = false;
    }
    void EnableGravity()
    {
        rb.useGravity = true;
    }

    void SetAudioVolume(float volume)
    {
        flyingSFX.volume=volume;
    }
    void Pause(Parameters param)
    {
        isPaused = param.GetBoolExtra("PauseVal", false);
    }

    // Update is called once per frame
    void Update()
    {
        isSteering = false;

        if (!isPaused && isPlayingFlyingSFX)
        {
            SetAudioVolume(1.0f);
        }
        else
        {
            SetAudioVolume(0.0f);
        }

        if (!isRelaxed && !isPaused) // Animation and Movement
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
