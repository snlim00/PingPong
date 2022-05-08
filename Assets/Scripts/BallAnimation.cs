using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private int dir = 1;

    public static float speedMultiplier = 9;
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed * speedMultiplier * dir * Time.deltaTime);
    }
}
