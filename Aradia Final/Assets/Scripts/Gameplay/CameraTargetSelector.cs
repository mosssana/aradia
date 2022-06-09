using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTargetSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform target = GetComponent<CinemachineVirtualCamera>().Follow;
        GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("CameraTarget").transform;
    }
}
