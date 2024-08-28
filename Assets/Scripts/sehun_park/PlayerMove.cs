using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
{
    public float moveSpeed = 5.0f;
    CharacterController cc;

    // �߷�
    float gravity = -9.81f;
    // y �ӷ�
    float yVelocity;
    // ���� �ʱ� �ӷ�
    public float jumpPower = 3;

    public GameObject cam;
    void Start()
    {
        cc = GetComponent<CharacterController>();
        // �� ���� ���� ī�޶� Ȱ��ȭ����.
        cam.SetActive(photonView.IsMine);
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // 1. Ű���� WASD Ű �Է��� ����
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            // 2. ������ ������.
            Vector3 dirH = transform.right * h;
            Vector3 dirV = transform.forward * v;
            Vector3 dir = dirH + dirV;

            dir.Normalize();

            // ���࿡ ���� ������ yVelocity�� 0���� �ʱ�ȭ
            if (cc.isGrounded)
            {
                yVelocity = 0;
            }

            // ���࿡ Space �ٸ� ������
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // yVelocity �� jumpPower�� ����
                yVelocity = jumpPower;
            }

            // yVelocity ���� �߷¿� ���ؼ� �����Ű��.
            yVelocity += gravity * Time.deltaTime;
            // dir.y�� yVelocity ���� ����
            dir.y = yVelocity;

            // �ڽ��� ������ �������� dir ����
            // dir = transform.TransformDirection(dir); ;

            // 3, �� �������� ��������.
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }

    }
}