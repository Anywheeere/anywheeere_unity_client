using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SimpleConnectionMgr : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Photon ȯ�漳���� ������� ������ ������ ������ �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        
    }

    // ������ ������ ������ �Ǹ� ȣ��Ǵ� �Լ�
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("������ ������ ����");

        // �κ� ����
        JoinLobby();
    }

    public void JoinLobby()
    {
        // �г��� ����
        PhotonNetwork.NickName = "�ڼ���";
        // �⺻ Lobby ����
        PhotonNetwork.JoinLobby();
    }

    // �κ� ������ �����ϸ� ȣ��Ǵ� �Լ�
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("�κ� ���� ����");

        JoinOrCreateRoom();
    }

    // Room�� ��������. ���࿡ �ش� Room�� ������ Room�� ����ڴ�.
    public void JoinOrCreateRoom()
    {
        // �� ���� �ɼ�
        RoomOptions roomOption = new RoomOptions();
        // �濡 ��� �� �� �ִ� �ִ� �ο� ������
        roomOption.MaxPlayers = 20;
        // �κ� ���� ���̰� �� ���ΰ�?
        roomOption.IsVisible = true;
        // �濡 ���� �����Ѱ�?
        roomOption.IsOpen = true;

        // Room ���� or ����
        PhotonNetwork.JoinOrCreateRoom("earrrth", roomOption, TypedLobby.Default);
    }

    // �� ���� �������� �� ȣ��Ǵ� �Լ�
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("�� ���� �Ϸ�");

        // ���� ������ ����� ������ ������ �ο��޴´�
        if (PhotonNetwork.IsMasterClient)
        {
            print("�� �÷��̾�� ������ Ŭ���̾�Ʈ�Դϴ�.");
        }
        else
        {
            print("�� �÷��̾�� ������ Ŭ���̾�Ʈ�� �ƴմϴ�.");
        }

        var properties = new ExitGames.Client.Photon.Hashtable
        {
            { "MasterClient", PhotonNetwork.LocalPlayer.ActorNumber }
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(properties);

        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { "isMaster", true }
        });
    }

    // �� ���� �������� �� ȣ��Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("�� ���� �Ϸ�");

        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("MasterClient", out object masterClient))
        {
            int masterClientActorNumber = (int)masterClient;
            bool isMaster = PhotonNetwork.LocalPlayer.ActorNumber == masterClientActorNumber;

            // �߰������� �÷��̾��� CustomProperties���� isMaster Ȯ��
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("isMaster", out object isMasterProp))
            {
                isMaster = (bool)isMasterProp;
            }
            else
            {
                isMaster = false; // �⺻������ �����Ͱ� �ƴ�
            }

            if (isMaster)
            {
                print("�� �÷��̾�� �������Դϴ�.");
            }
            else
            {
                print("�� �÷��̾�� �����Ͱ� �ƴմϴ�.");
            }
        }
        else
        {
            print("������ Ŭ���̾�Ʈ ������ �����ϴ�.");
        }

        // ��Ƽ�÷��� ������ ��� �� �ִ� ����
        // GameScene���� �̵�
        PhotonNetwork.LoadLevel("CesiumGoogleMapsTiles");
        // PhotonNetwork.LoadLevel("MapTestScene");
    }
}
