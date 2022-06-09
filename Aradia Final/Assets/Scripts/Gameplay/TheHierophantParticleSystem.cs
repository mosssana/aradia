using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHierophantParticleSystem : MonoBehaviour
{
    GameObject target;

    public GameObject Target
    {
        set { target = value; }
    }

    Vector3 scale;
    float FixeScaleX;
    float FixeScaleY;
    float FixeScaleZ;

    void Start()
    {
        scale = transform.localScale;
        FixeScaleX = Mathf.Abs(scale.x);
        FixeScaleY = Mathf.Abs(scale.y);
        FixeScaleZ = Mathf.Abs(scale.z);

        EventManager.AddListener(EventName.RoomChanged, Destroy);
    }

    void Update()
    {
        // prevents object from being flipped by parent object
        transform.localScale = new Vector3(FixeScaleX / transform.parent.localScale.x, FixeScaleY / transform.parent.localScale.y, FixeScaleZ / transform.parent.localScale.z);
    }

    void FixedUpdate()
    {
        if (target != null) transform.rotation = Quaternion.Euler(0f, 0f, CalculateAngle());
        else Destroy();
    }

    float CalculateAngle()
    {
        return Mathf.Rad2Deg * Mathf.Atan2((target.transform.position.y - transform.position.y), (target.transform.position.x - transform.position.x));
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
