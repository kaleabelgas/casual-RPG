using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    public static CameraShake Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private float shakeClock;

    // Start is called before

    private void Awake()
    {
        Instance = this;
    }

    public void ShakeCamera(float intensity, float timer)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeClock = timer;
    }

    private void Update()
    {

        if(shakeClock > 0)
        {
            shakeClock -= Time.deltaTime;
            if(shakeClock <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            }
        }
    }
}
