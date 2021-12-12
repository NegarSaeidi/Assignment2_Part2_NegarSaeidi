using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum movinPlatformDirection
{
    HORIZONTAL,
    VERTICAL,
    DIAGONAL_UP,
    DIAGONAL_DOWN
}
public class MovingPlatformController : MonoBehaviour
{
    public movinPlatformDirection direction;
    [Range(0.1f, 10.0f)]
    public float speed;
    [Range(0.1f, 10.0f)]
    public float distance;

    public bool isLooping;
    private Vector2 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        isLooping = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        //Vector3 disMag = transform.position - new Vector3(startingPos.x,startingPos.y);
        // if (Mathf.Abs(disMag.magnitude) >= distance-0.1f)
        //     isLooping = false;
        // if (isLooping)
        // {
        switch (direction)
        {
            case movinPlatformDirection.HORIZONTAL:
                transform.position = new Vector2(startingPos.x + Mathf.PingPong(Time.time * speed, distance), transform.position.y);
                break;
            case movinPlatformDirection.VERTICAL:
                transform.position = new Vector2(transform.position.x, startingPos.y + Mathf.PingPong(Time.time * speed, distance));
                break;
            case movinPlatformDirection.DIAGONAL_UP:
                transform.position = new Vector2(startingPos.x + Mathf.PingPong(Time.time * speed, distance), startingPos.y + Mathf.PingPong(Time.time * speed, distance));
                break;
            case movinPlatformDirection.DIAGONAL_DOWN:
                transform.position = new Vector2(startingPos.x - Mathf.PingPong(Time.time * speed, distance), startingPos.y - Mathf.PingPong(Time.time * speed, distance));
                break;

        }
        //}

    }
}