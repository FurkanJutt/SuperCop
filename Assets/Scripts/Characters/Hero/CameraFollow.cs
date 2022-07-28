using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float minX, maxX;

    private Transform hero;
    private Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!hero)
            return;

        tempPos = transform.position;
        tempPos.x = hero.position.x;

        if (tempPos.x < minX)
            tempPos.x = minX;

        if (tempPos.x > maxX)
            tempPos.x = maxX;

        transform.position = tempPos;
    }
}
