using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GotoCityAdv : MonoBehaviour
{
    void Update()
    {
        // B Ű�� ���ȴ��� Ȯ��
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Photon ��Ʈ��ũ�� ���� ���� �ε�
            PhotonNetwork.LoadLevel("03_CesiumSanFrancisco");
        }
    }
}