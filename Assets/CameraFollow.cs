using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Vector3 target;

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target + offset, Time.deltaTime * 3f);
    }

    public void SetTarget()
    {
        target += Vector3.up * 0.5f;
    }
}
