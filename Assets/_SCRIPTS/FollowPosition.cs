using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] Vector3 offset;

    [SerializeField] float smoothTime = 0.3f;

    public enum movementType { exact, smooth }
    public movementType m_type = movementType.exact;

    private Vector2 velocity = Vector2.zero;

    private void Update()
    {
        if(m_type == movementType.exact)
        {
            //transform.position = playerPos.position;
            transform.position = new Vector3(playerPos.position.x + offset.x, playerPos.position.y + offset.y, playerPos.position.z + offset.z);
        }
        else
        {
            transform.position = Vector2.SmoothDamp(transform.position, new Vector2(playerPos.position.x, playerPos.position.y), ref velocity, smoothTime);
        }
    }
}
