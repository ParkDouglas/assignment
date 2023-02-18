using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float minXClamp;
    public float maxXClamp;

    public float minYClamp;
    public float maxYClamp;

    private void LateUpdate()
    {
        Vector3 cameraPosition;

        cameraPosition = transform.position;

        cameraPosition.x = Mathf.Clamp(player.transform.position.x, minXClamp, maxXClamp);

        cameraPosition.y = Mathf.Clamp(player.transform.position.y, minYClamp, maxYClamp);

        transform.position = cameraPosition;
    }

}
