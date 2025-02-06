using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;
    public float shakeDuration = 10f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    public bool shaketrue = false;
    Vector3 originalPos;
    float originalShakeDuration; 

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalShakeDuration = shakeDuration; 
    }
    void Update()
    {
        if (shaketrue)
        {
            originalPos = camTransform.localPosition;
            if (shakeDuration > 0)
            {
                Vector3 shakePos = originalPos + Random.insideUnitSphere * shakeAmount;
                shakePos.z = originalPos.z; 
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, shakePos, Time.deltaTime * 3);

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = originalShakeDuration;
                camTransform.localPosition = originalPos;
                shaketrue = false;
            }
        }
    }

    public void shakecamera()
    {
        originalPos = camTransform.localPosition;
        shaketrue = true;
    }
}