using UnityEngine;
using Unity.Cinemachine;

public class CameraSwapper : MonoBehaviour
{
    public static CameraSwapper instance;

    [SerializeField] CinemachineCamera main;
    [SerializeField] CinemachineCamera follow;

    private void Start()
    {
        instance = this;
    }

    public void SwitchToMain()
    {
        main.Prioritize();
    }

    public void SwitchToFollow()
    {
        follow.Prioritize();
    }
}
