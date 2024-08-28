using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjRotate : MonoBehaviour
{
    public float rotSpeed = 200.0f;

    // ȸ�� ��
    float rotX;
    float rotY;

    // ȸ�� ���� ����
    public bool useRotX;
    public bool useRotY;

    public PhotonView pv;
    void Start()
    {
        
    }


    void Update()
    {
        if(pv.IsMine)
        {
            // ���콺�� �������� �޾ƿ���.
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");

            // ȸ�� ���� ���� (����)
            if (useRotX) rotX += my * rotSpeed * Time.deltaTime;
            if (useRotY) rotY += mx * rotSpeed * Time.deltaTime;

            // rotX�� �� ����
            rotX = Mathf.Clamp(rotX, -80, 80);

            // ������ ȸ��
            transform.localEulerAngles = new Vector3(-rotX, rotY, 0);

        }
    }
}
