using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField]
    private Transform _cameraTransform;
    [SerializeField]
    private float shakeFrequency;

    private Vector3 _originalPosOfCam;
    // Start is called before the first frame update
    void Start()
    {
        _originalPosOfCam = _cameraTransform.position;
    }

    public void CameraShakeStart()
    {
        StartCoroutine(CameraShakeRoutine());
    }

    IEnumerator CameraShakeRoutine()
    {
        yield return new WaitForEndOfFrame();
        _cameraTransform.position = _originalPosOfCam + Random.insideUnitSphere * shakeFrequency;
        yield return new WaitForSeconds(.05f);
        _cameraTransform.position = _originalPosOfCam;
    }
}
