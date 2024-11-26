using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

[RequireComponent(typeof(CinemachineImpulseSource))]
[RequireComponent(typeof(PlaySFX))]
public class ShakeCameraWithinDistance : MonoBehaviour
{
    GameObject player;

    CinemachineImpulseSource _source;

    PlaySFX pSFX;

    [SerializeField] float minDistance = 30f;
    [SerializeField] List<Transform> positionRef;

    bool inRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _source = GetComponent<CinemachineImpulseSource>();
        pSFX = GetComponent<PlaySFX>();
    }

    private void Update()
    {
        foreach(Transform pos in positionRef)
        {
            float distance = Vector2.Distance(pos.position, player.transform.position);

            if (distance < minDistance) { inRange = true; }
            else { inRange = false; }
        }
    }

    public void ShakeCamera()
    {
        if (inRange)
        {
            CameraShakeManager.instance.CameraShake(_source);

            pSFX.PlaySFXOnce();
        }
    }
}
