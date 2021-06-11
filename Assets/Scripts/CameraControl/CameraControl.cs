using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float OFFSET_X;
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x + OFFSET_X, transform.position.y, -10f);
    }
}
