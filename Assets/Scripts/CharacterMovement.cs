using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    Transform head;
    Animator characterController;
    float horizontalValue, verticalValue;
    float moveSpeed = 5;

    private void Start()
    {
        characterController = GetComponent<Animator>();
    }
    private void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");
        NavigateCharacter();
        Moving();
    }
    void Moving()
    {
        if (!AnyAnimationIsPlaying())
        {
            if (horizontalValue != 0 || verticalValue != 0)
            {
                characterController.SetBool("bStopMoving", false);
                characterController.SetBool("bMoving", true);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            else if (horizontalValue == 0 && verticalValue == 0)
            {
                characterController.SetBool("bStopMoving", true);
                characterController.SetBool("bMoving", false);
            }
        }
    }
    string[] animationNames = {"Attack", "Dash", "React", "Die"};
    bool AnyAnimationIsPlaying()
    {
        for (int i = 0; i < animationNames.Length; i++)
        {
            if (characterController.GetCurrentAnimatorStateInfo(0).IsName(animationNames[i]))
                return true;
        }
        return false;
    }
    void NavigateCharacter()
    {
        if (verticalValue > 0)
        {
            SetTheAngles(head.forward);
            SlerpTo();
        }
        else if (verticalValue < 0)
        {
            SetTheAngles(-head.forward);
            SlerpTo();
        }

        if (horizontalValue > 0)
        {
            SetTheAngles(head.right);
            SlerpTo();
        }
        else if (horizontalValue < 0)
        {
            SetTheAngles(-head.right);
            SlerpTo();
        }
    }

    Quaternion theOldAngle, theTargetAngle;
    void SetTheAngles(Vector3 dir)
    {
        if (timeElapse == 0)
        {
            theOldAngle = transform.rotation;//miss
        }
        theTargetAngle = Quaternion.LookRotation(dir);
    }

    float timeElapse;
    void SlerpTo()
    {
        timeElapse += Time.deltaTime;
        if (timeElapse > 1)
        {
            timeElapse = 0;//miss
            return;
        }
        transform.rotation = Quaternion.Slerp(theOldAngle, theTargetAngle, timeElapse);
    }

    private void LateUpdate()
    {
        SetUpHead();
    }

    void SetUpHead()
    {
        head.position = transform.position + transform.up * 1.5f;
    }


}
