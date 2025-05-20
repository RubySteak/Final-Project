using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInCircle : MonoBehaviour
{
    public float speed = 2f;
    public float radius = 3f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float angle = Time.time * speed;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        transform.position = startPos + new Vector3(x, y, 0f);
    }
}