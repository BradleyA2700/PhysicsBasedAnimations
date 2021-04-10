using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneControler : MonoBehaviour
{
    public Transform targetBone;
    ConfigurableJoint jointCtrl;
    public bool mirror;
    Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
        jointCtrl = GetComponent<ConfigurableJoint>();
    }
    void Update()
    {
        if (targetBone != null)
        {

            Vector3 forwardDir = Vector3.Cross(jointCtrl.axis, jointCtrl.secondaryAxis).normalized;
            Vector3 upDir = jointCtrl.secondaryAxis;
            Quaternion worldSpaceToJointSpace = Quaternion.LookRotation(forwardDir, upDir);

            Quaternion newRotation = worldSpaceToJointSpace;
            newRotation *= Quaternion.Inverse(targetBone.localRotation) * startRotation;
            newRotation *= worldSpaceToJointSpace;
            newRotation = newRotation.normalized;
            if (mirror)
            {
                jointCtrl.targetRotation = Quaternion.Inverse(newRotation);
            }
            jointCtrl.targetRotation = newRotation;
                
        }
        else { Debug.Log("No bone for " + transform.name); 
        }
         
    }
}
