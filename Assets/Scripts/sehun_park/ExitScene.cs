using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ExitScene : MonoBehaviour
{
    private PhotonView photonView;

    void Awake()
    {
        // PhotonView ������Ʈ�� ã�� �ʱ�ȭ
        photonView = GetComponent<PhotonView>();

        // photonView�� null���� Ȯ��
        if (photonView == null)
        {
            Debug.LogError("PhotonView ������Ʈ�� �� ���� ������Ʈ�� �����ϴ�.");
        }
    }

    void Update()
    {
        // B Ű�� ���ȴ��� Ȯ��
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsMasterClient)
        {
            // ��� �÷��̾�� �� ���� ��û
            if (photonView != null)
            {
                photonView.RPC("ChangeScene", RpcTarget.All, "CesiumGoogleMapsTiles_Beta_X");
            }
        }
    }

    [PunRPC]
    void ChangeScene(string sceneName)
    {
        // �� �ε�
        PhotonNetwork.LoadLevel(sceneName);
    }
}