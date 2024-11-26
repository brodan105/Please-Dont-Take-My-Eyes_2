using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform camPos;

    CinemachineCamera cam;

    [Header("Properties")]
    [SerializeField] Vector3 desiredCamPos;
    [SerializeField] float tTime = 0.5f;
    [SerializeField] float cameraFOV;
    [SerializeField] float cameraTransitionTime = 1f;

    bool startTransition = false;

    private void Awake()
    {
        cam = camPos.GetComponent<CinemachineCamera>();
    }

    private void Update()
    {

        if (!startTransition) return;

        // Change position
        camPos.localPosition = Vector3.Lerp(camPos.localPosition, desiredCamPos, tTime * Time.deltaTime);

        if(cameraFOV > 0)
        {
            // Change FOV
            cam.Lens.OrthographicSize = Mathf.Lerp(cam.Lens.OrthographicSize, cameraFOV, tTime * Time.deltaTime);

            //StartCoroutine(ChangeFOV(cameraFOV, tTime));
        }

        float camDistance = Vector3.Distance(camPos.localPosition, desiredCamPos);
        if (camDistance < 0.5f)
        {
            startTransition = false;
            return;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        

        if(collision.tag == "Player")
        {
            startTransition = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            startTransition = false;
        }
    }

    IEnumerator ChangeFOV(float endFOV, float duration)
    {
        float startFOV = cam.Lens.OrthographicSize;
        float time = 0;
        while (time < duration)
        {
            cam.Lens.OrthographicSize = Mathf.Lerp(startFOV, endFOV, time / duration);
            yield return null;
            time += Time.deltaTime;
        }
    }

    IEnumerator ChangePosition(Vector3 endPos, float duration)
    {
        Vector3 startPos = camPos.localPosition;
        float time = 0;
        while (time < duration)
        {
            camPos.localPosition = Vector3.Lerp(startPos, endPos, time / duration);
            yield return null;
            time += Time.deltaTime;
        }
    }
}
