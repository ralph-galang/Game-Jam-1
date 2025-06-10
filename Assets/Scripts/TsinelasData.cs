using UnityEngine;

[System.Serializable]

public class TsinelasData
{
    [SerializeField] public float forwardSpeed = 100.0f;

    [SerializeField] public float maxForwardSpeed = 100.0f;
    [SerializeField] public float speed = 10.0f;
    [SerializeField] public float dashSpeed = 100.0f;
    [SerializeField] public float spinSpeed = 10.0f;
    [SerializeField] public float steeringSpeed = 10.0f;
    [SerializeField] public float courseCorrect = 2.0f;
}
