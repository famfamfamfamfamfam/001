using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimation : MonoBehaviour
{
    Animator characterController;
    private void Start()
    {
        characterController = GetComponent<Animator>();
    }

    private void Update()
    {
        ToAttack();
        ToDash();
    }

    void ToAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            characterController.SetTrigger("trAttack");
        }
    }
    int dashFrameCount;
    bool dashCondition;
    void ToDash()
    {
        if (Input.GetMouseButtonDown(1))
        {
            characterController.SetTrigger("trDash");
            dashFrameCount = 1;
            dashCondition = dashFrameCount == 1;
        }
        if (dashCondition)
        {
            transform.position += transform.forward * 15 * Time.deltaTime;
            dashFrameCount++;
            dashCondition = dashFrameCount < 47;
        }
        else
        {
            dashFrameCount = 0;
        }
        Debug.Log(dashFrameCount);
    }

    void ToReact()
    {
        characterController.SetTrigger("trReact");
    }

    void ToDie()
    {
        characterController.SetTrigger("trDie");
    }

}
