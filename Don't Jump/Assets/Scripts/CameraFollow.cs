using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = .1f;
    public Vector3 offset;
    public Queue skyQueue;
    public float currPosX;
    public Object startSky;
    public const float skyOffset = 21.6f;
    private const int skiesAmount = 4;

    void Start()
    {
        currPosX = 0f;
        skyQueue = new Queue();
    }

    void Update()
    {
        SkyInspect();
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.y = 0;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;
        //transform.position = target.position + offset;
    }

    private void SkyInspect()
    {
        if(target.position.x > currPosX + skyOffset/2)
        {
            currPosX += skyOffset;
            Object tmp = Instantiate(startSky, new Vector3(currPosX + skyOffset, 0, 0), Quaternion.identity);
            skyQueue.Enqueue(tmp);
        }

        if(skyQueue.Count >= skiesAmount)
        {
            Destroy((Object)skyQueue.Dequeue());
            
        }
    }

}
