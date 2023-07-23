using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKActions : MonoBehaviour
{
    [SerializeField] private KeyCode actionButton;
    [SerializeField] private Transform leftHandTarget, rightHandTarget;
    [SerializeField] private Vector3 leftHandPosition = Vector3.zero, rightHandPosition = Vector3.zero;
    [SerializeField] private Quaternion leftHandRotation = Quaternion.identity, rightHandRotation = Quaternion.identity;
    [SerializeField] private Transform overlapSphere;
    [SerializeField] private float _radius = 0.4f;
    [SerializeField] private TwoBoneIKConstraint leftHandIK, rightHandIK;
    [SerializeField] private TwistChainConstraint twistChainConstraint;
    [SerializeField] private Transform tipTarget;
    [SerializeField] private Vector3 startRotation;
    [SerializeField] private Vector3 endRotation;


    private InteractableObject currentInteractableObject;

    private static float InterpolationRate = 10f;
    private float interpolatedWeightR = 0f;
    private float interpolatedWeightL = 0f;
    private float interpolatedWeightB = 0f;
    private bool isIKOn;

    private void Update()
    {
        if (Input.GetKey(actionButton))
        {
            TryGrabInterctableObject();
            
        }

        if (Input.GetKeyUp(actionButton))
        {
            TurnOffIK();
            isIKOn = false;
        }

        if (isIKOn)
        {
            interpolatedWeightB = Mathf.Lerp(interpolatedWeightB, 1, Time.deltaTime * InterpolationRate);
        }
        else
        {
            interpolatedWeightB = Mathf.Lerp(interpolatedWeightB, 0, Time.deltaTime * InterpolationRate);
            
        }

        

        if (leftHandPosition != Vector3.zero)
        {
            interpolatedWeightL = Mathf.Lerp(interpolatedWeightL, 1, Time.deltaTime * InterpolationRate);
            leftHandTarget.position = leftHandPosition;
            leftHandTarget.rotation = leftHandRotation;
     
        }

        else
        {
            interpolatedWeightL = Mathf.Lerp(interpolatedWeightL, 0, Time.deltaTime * InterpolationRate);
            
        }

        if (rightHandPosition != Vector3.zero)
        {
            interpolatedWeightR = Mathf.Lerp(interpolatedWeightR, 1, Time.deltaTime * InterpolationRate);
            rightHandTarget.position = rightHandPosition;
            rightHandTarget.rotation = rightHandRotation;

        }

        else
        {
            interpolatedWeightR = Mathf.Lerp(interpolatedWeightR, 0, Time.deltaTime * InterpolationRate);
            
        }

        leftHandIK.weight = interpolatedWeightL;
        rightHandIK.weight = interpolatedWeightR;
        twistChainConstraint.weight = interpolatedWeightB;
    }


    private void TryGrabInterctableObject()
    {
        Collider[] colliders = Physics.OverlapSphere(overlapSphere.position, _radius, LayerMask.GetMask("InterctableObject"));
        if (colliders.Length <= 0)
        {
            TurnOffIK();
        }

        else
        {
            isIKOn = true;
        }
        foreach (Collider collider in colliders)
        {
            InteractableObject interactableObject = collider.GetComponent<InteractableObject>();
            currentInteractableObject = interactableObject;
            if (currentInteractableObject.leftHandTransform != null)
            {
                leftHandPosition = currentInteractableObject.leftHandTransform.position;
                leftHandRotation = currentInteractableObject.leftHandTransform.rotation;
            }
            if (currentInteractableObject.rightHandTransform != null)
            {
                rightHandPosition = currentInteractableObject.rightHandTransform.position;
                rightHandRotation = currentInteractableObject.rightHandTransform.rotation;
            }
            if (collider.GetComponent<LeverSystem>() != null)
            {
                LeverSystem lever = collider.GetComponent<LeverSystem>();
                twistChainConstraint.weight = interpolatedWeightB;
                lever.ChangeLeverState(Input.GetAxis("Mouse Y") / 50f);
                tipTarget.rotation = Quaternion.Lerp(Quaternion.Euler(startRotation), Quaternion.Euler(endRotation), lever.leverState);
            }
            
        }
    }
    private void TurnOffIK()
    {
        currentInteractableObject = null;
        leftHandPosition = Vector3.zero;
        rightHandPosition = Vector3.zero;
        leftHandRotation = Quaternion.identity;
        rightHandRotation = Quaternion.identity;
    }
}
