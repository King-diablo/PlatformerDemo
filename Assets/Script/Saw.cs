using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float speed;
    [SerializeField] float waitTimer;
    [SerializeField] bool isStatic;
    int currentPosition;
    float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStatic)
        {
            if(points.Length > 0)
            {
                transform.position = Vector3.Lerp(transform.position, points[currentPosition].transform.position, speed * Time.deltaTime);
                NewPosition();
            }
        }
    }

    private void NewPosition()
    {
        if(waitTime < waitTimer)
        {
            waitTime += Time.deltaTime;
        }
        else
        {
            waitTime = 0;
            currentPosition = (currentPosition + 1) % points.Length;
        }
    }
}
