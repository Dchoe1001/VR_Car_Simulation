using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class anchorCamera : MonoBehaviour
{
    public Transform anchorPoint;

    void Update()
    {
        transform.position = anchorPoint.position;
        Quaternion headsetRotation = InputTracking.GetLocalRotation(XRNode.Head);

        // set the camera position to be the same as the anchor point

        ///Quaternion newRotation = Quaternion.Euler(0f, -90f, 0f) * headsetRotation;

        anchorPoint.rotation = headsetRotation;
    }
}
