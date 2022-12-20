using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    private Transform cameraArm;

    public static PlayerCamera Instance = null;
    private CinemachineBasicMultiChannelPerlin _actionPerlin = null;
    private CinemachineVirtualCamera playerCamera = null;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        cameraArm = transform.parent.GetComponent<Transform>(); 
        playerCamera = cameraArm.GetChild(0).GetComponent<CinemachineVirtualCamera>();
        _actionPerlin = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        LookAround();
    }

    private void LookAround()
    {
        Vector2 mousedelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mousedelta.y;

        if(x < 180)
        {
            x = Mathf.Clamp(x, -1, 70);
        }
        else
        {
            x = Mathf.Clamp(x, 335, 361);
        }

        cameraArm.rotation = Quaternion.Euler(camAngle.x - mousedelta.y, camAngle.y + mousedelta.x, camAngle.z);
    }

    public void ShakeCam(float intensity, float time)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCamCoroutine(intensity, time));
    }

    IEnumerator ShakeCamCoroutine(float intensity, float endTime)
    {
        _actionPerlin.m_AmplitudeGain = intensity;
        float currentTime = 0f;

        while (currentTime < endTime)
        {
            yield return new WaitForEndOfFrame();

            _actionPerlin.m_AmplitudeGain = Mathf.Lerp(intensity, 0, currentTime / endTime);
            currentTime += Time.deltaTime;
        }

        if (_actionPerlin != null)
            _actionPerlin.m_AmplitudeGain = 0;
    }
}
