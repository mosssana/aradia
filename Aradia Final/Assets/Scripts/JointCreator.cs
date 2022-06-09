using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointCreator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> allBones;

    private void Start() {
        CreateJoints();
    }

    [ContextMenu("CreateJoints")]
    private void CreateJoints()
    {
        allBones = new List<GameObject>();
        GetBone(transform);

        Rigidbody2D previousRB = null;

        for (int i = 0; i < allBones.Count; i++)
        {
            allBones[i].AddComponent<HingeJoint2D>();

            if (previousRB != null){
                allBones[i].GetComponent<HingeJoint2D>().connectedBody = previousRB;
            } else {
                allBones[i].GetComponent<HingeJoint2D>().connectedBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            }

            previousRB = allBones[i].GetComponent<Rigidbody2D>();
        }
    }

    private void GetBone(Transform bone)
    {
        allBones.Add(bone.gameObject);
        foreach (Transform child in bone)
        {
            GetBone(child);
        }
    }
}
