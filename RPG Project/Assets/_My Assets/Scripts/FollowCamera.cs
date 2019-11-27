using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }
}
