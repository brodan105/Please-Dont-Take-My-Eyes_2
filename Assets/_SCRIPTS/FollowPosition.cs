using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] Vector3 offset;

    private void Update()
    {
        //transform.position = playerPos.position;
        transform.position = new Vector3(playerPos.position.x + offset.x, playerPos.position.y + offset.y, playerPos.position.z + offset.z);
    }
}
