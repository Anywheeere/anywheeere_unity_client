using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviourPun
{
    public GameObject cubeFactory;
    void Start()
    {

    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // 1�� Ű ������
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Ű ����");
                // ī�޶��� �� �������� 5��ŭ ������ ��ġ�� ������.
                Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 10;
                // ť�� ���忡�� ť�긦 ����, ��ġ, ȸ��
                PhotonNetwork.Instantiate("Cube", pos, Quaternion.identity);
            }
        }
    }
}
