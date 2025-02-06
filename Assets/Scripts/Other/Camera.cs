using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Vector3 offset;
    Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindWithTag(Tag.Player).transform;
        offset = transform.position - playerTransform.position;
    }

    void Update()
    {
        transform.position = playerTransform.position + offset;

        //»ñÈ¡¹öÂÖ
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        UnityEngine.Camera.main.fieldOfView += scroll;
        UnityEngine.Camera.main.fieldOfView = Mathf.Clamp(UnityEngine.Camera.main.fieldOfView, 37, 70);
    }
}
