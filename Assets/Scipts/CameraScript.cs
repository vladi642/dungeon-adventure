using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target; // Следван целев обект (играча)
    public float smoothSpeed = 0.125f; // Скорост на следване

    void LateUpdate()
    {
        if (target != null)
        {
            // Изчисляваме новата позиция на камерата като използваме SmoothDamp
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Преместваме камерата към новата позиция
            transform.position = smoothedPosition;
        }
    }
}
