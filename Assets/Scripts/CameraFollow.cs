using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10);

    void Start()
    {
        if (target == null)
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void LateUpdate()
    {
        if (target == null)
        return;

        Vector3 targetpos = new Vector3( 0,target.transform.position.y,0);
        transform.position = targetpos + offset;
    }
}
