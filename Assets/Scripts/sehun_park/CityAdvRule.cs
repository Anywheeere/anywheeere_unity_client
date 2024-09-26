using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CityAdvRule : MonoBehaviourPun
{
    // Ÿ�̸� �ð� (1��)
    private int minutes = 1;
    private int seconds = 0;

    // Ÿ�̸� UI ����� Text
    public Text timerText;
    public Text countdownText; // ī��Ʈ�ٿ� �ؽ�Ʈ

    // Ÿ�̸Ӱ� ���� ������ ����
    private bool timerRunning = false;

    private void Start()
    {
        // ������ Ŭ���̾�Ʈ������ ī��Ʈ�ٿ� ����
        if (PhotonNetwork.IsMasterClient)
        {
            // 5�� ī��Ʈ�ٿ� �� Ÿ�̸� ����
            photonView.RPC(nameof(StartCountdownRPC), RpcTarget.AllBuffered);
        }
    }

    // ī��Ʈ�ٿ� ������ ���� RPC ȣ��
    [PunRPC]
    private void StartCountdownRPC()
    {
        StartCoroutine(StartCountdown());
    }

    // 5�� ī��Ʈ�ٿ� �ڷ�ƾ
    private IEnumerator StartCountdown()
    {
        int countdown = 5;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString(); // ī��Ʈ�ٿ� �ؽ�Ʈ ������Ʈ
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = ""; // ī��Ʈ�ٿ��� ������ �ؽ�Ʈ �����

        StartTimer(); // Ÿ�̸� ����
    }

    // Ÿ�̸� ������ ���� RPC ȣ��
    private void StartTimer()
    {
        photonView.RPC(nameof(StartTimerRPC), RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void StartTimerRPC()
    {
        if (!timerRunning)
        {
            timerRunning = true;
            InvokeRepeating(nameof(UpdateTimer), 1.0f, 1.0f);
        }
    }

    // �� �ʸ��� Ÿ�̸Ӹ� ������Ʈ
    private void UpdateTimer()
    {
        if (!timerRunning) return;

        if (seconds == 0)
        {
            if (minutes == 0)
            {
                timerRunning = false;
                CancelInvoke(nameof(UpdateTimer));
                TimeUp();
                return;
            }
            else
            {
                minutes--;
                seconds = 59;
            }
        }
        else
        {
            seconds--;
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    private void TimeUp()
    {
        Debug.Log("�ð� ����");
        ResultManager.instance.ShowResults();  // ��� ���
    }
}