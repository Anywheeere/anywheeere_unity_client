using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SetChild : MonoBehaviour
{
    // ���� ������Ʈ�� ���� ī�޶��� �̸�
    private const string cameraName = "DynamicCamera";

    // �÷��̾��� ��ġ�� ������ �÷��̾� ������ ��ġ�� �ݰ�
    public float radius = 5.0f;

    void Start()
    {
        // �濡 ������ �� �÷��̾� ��ġ ����
        SetPlayerPositions();
    }

    private void SetPlayerPositions()
    {
        // �±װ� "MainCamera"�� ������Ʈ�� ã���ϴ�.
        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");

        if (cameraObject != null)
        {
            // ī�޶� ������Ʈ�� ã������, ���� ������Ʈ�� ī�޶��� �ڽ����� �����մϴ�.
            if (cameraObject.name == cameraName)
            {
                // ���� ��� �÷��̾ ��ȸ�ϸ鼭 ��ġ�� �����մϴ�.
                foreach (Player player in PhotonNetwork.PlayerList)
                {
                    // ������ Ŭ���̾�Ʈ �ĺ�
                    if (player.ActorNumber == PhotonNetwork.MasterClient.ActorNumber)
                    {
                        // ������ �÷��̾�� (0, 0, 6) ��ġ�� ��ġ�մϴ�.
                        if (PhotonNetwork.LocalPlayer.ActorNumber == player.ActorNumber)
                        {
                            transform.SetParent(cameraObject.transform);
                            transform.localPosition = new Vector3(0, 0, 6);
                            transform.localRotation = Quaternion.identity;
                            print("������ �÷��̾� ��ġ ���� �Ϸ�");
                        }
                    }
                    else
                    {
                        // ������ Ŭ���̾�Ʈ�� �ƴ� �÷��̾��� ��ġ�� �����մϴ�.
                        if (PhotonNetwork.LocalPlayer.ActorNumber == player.ActorNumber)
                        {
                            transform.SetParent(cameraObject.transform);

                            // ������ �÷��̾��� ��ġ�� �������� ���� ��ġ
                            Vector3 masterPosition = new Vector3(0, 0, 6);
                            float angle = Random.Range(0f, 360f);
                            float x = masterPosition.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
                            float z = masterPosition.z + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
                            Vector3 playerPosition = new Vector3(x, 0, z);

                            transform.localPosition = playerPosition;
                            transform.localRotation = Quaternion.identity;
                            print("�����Ͱ� �ƴ� �÷��̾� ��ġ ���� �Ϸ�");
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning($"�±װ� 'MainCamera'�� ������Ʈ�� ������, �̸��� '{cameraName}'�� �ƴմϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("�±װ� 'MainCamera'�� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}