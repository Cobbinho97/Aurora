using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public Vector2 velocity;

    private float baseY;

    void Start()
    {
        baseY = transform.position.y;
    }

    void Update()
    {
        float targetY = Mathf.PingPong(Time.time * 0.2f, 0.4f);
        Vector2 targetPosition = new Vector2(transform.position.x, baseY + targetY);
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, 1f);
    }
}
