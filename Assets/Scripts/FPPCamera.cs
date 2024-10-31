using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPCamera : MonoBehaviour
{
    [SerializeField]
    Transform theWall;
    Camera thisCam;
    private void Start()
    {
        thisCam = GetComponent<Camera>();
        transform.position = theWall.position - Vector3.forward * 15;
    }
    private void Update()
    {
        //CaculateAxis();
        //MoveSight();
        Shooting();
        KickBack();
    }

    Vector3 mousePos, axis;
    void CaculateAxis()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 0.02f;
        mousePos = thisCam.ScreenToWorldPoint(mousePos);
        axis = mousePos - transform.position;
        axis.z = 0;
        axis = axis.normalized;
    }
    void MoveSight()
    {
        transform.position += axis * 5 * Time.deltaTime;
    }

    Ray shootingRay;
    RaycastHit hit;
    void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shootingRay = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(shootingRay, out hit, 115)) //them layermask
            {
                Debug.Log("ban trung");
            }
            kick = true;
        }
    }
    bool kick = false;
    int frameCount = 1;
    void KickBack()
    {
        if (kick)
        {
            if (frameCount < 21)
            {
                transform.rotation *= Quaternion.Euler(-1f / 20, 0f, 0f);
                frameCount++;
            }
            else if (frameCount < 51)
            {
                transform.rotation *= Quaternion.Euler(1f / 30, 0f, -0f);
                frameCount++;
            }
            else
            {
                frameCount = 1;
                kick = false;
            }
        }
    }
}
