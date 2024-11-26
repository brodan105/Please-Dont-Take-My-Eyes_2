using System.Collections;
using UnityEngine;

public class SpawnedAttack : MonoBehaviour
{
    Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.2f * Time.deltaTime);

        StartCoroutine(Decay());
    }

    private IEnumerator Decay()
    {
        yield return new WaitForSeconds(5);
        Destroy(this);
    }
}
