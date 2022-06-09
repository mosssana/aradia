using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantainFlip : MonoBehaviour
{
    Vector3 scale;
    float FixeScaleX;
    float FixeScaleY;
    float FixeScaleZ;

    public bool maintainRotation = false;

    Quaternion ogRotation;

    void Start()
    {
        scale = transform.localScale;
        FixeScaleX = scale.x;
        FixeScaleY = scale.y;
        FixeScaleZ = scale.z;

        ogRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // prevents object from being flipped by parent object
        transform.localScale = new Vector3(FixeScaleX / transform.parent.localScale.x, FixeScaleY / transform.parent.localScale.y, FixeScaleZ / transform.parent.localScale.z);

        if (maintainRotation) transform.rotation = ogRotation;
    }
}
