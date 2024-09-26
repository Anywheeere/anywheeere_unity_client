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

    // ī��Ʈ�ٿ��� ����� Text
    public Text countdownText;

    // Ÿ�̸Ӱ� ���� ������ ����
    private bool timerRunning = false;

    private void Start()
    {
        // ������ Ŭ���̾�Ʈ������ ī��Ʈ�ٿ� ����
        if (PhotonNetwork.IsMasterClient)
        {
            // 5�� ���� ī��Ʈ�ٿ� �� Ÿ�̸� ����
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
            // ī��Ʈ�ٿ� �ؽ�Ʈ ������Ʈ
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        // ī��Ʈ�ٿ� �ؽ�Ʈ �ı�
        Destroy(countdownText.gameObject);

        // Ÿ�̸� ����
        StartTimer();
    }

    // Ÿ�̸� ������ ���� RPC ȣ�� (��� Ŭ���̾�Ʈ���� Ÿ�̸� ����)
    private void StartTimer()
    {
        photonView.RPC(nameof(StartTimerRPC), RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void StartTimerRPC()
    {
        // Ÿ�̸� ���� ����
        timerRunning = true;
        // �� 1�ʸ��� UpdateTimer() ȣ��
        InvokeRepeating(nameof(UpdateTimer), 1.0f, 1.0f);
    }

    // �� �ʸ��� Ÿ�̸Ӹ� ������Ʈ
    private void UpdateTimer()
    {
        if (!timerRunning) return;

        // �ʰ� 0�̸� ���� ���̰�, �ʸ� 59�� ����
        if (seconds == 0)
        {
            if (minutes == 0)
            {
                // Ÿ�̸Ӱ� ����Ǹ�
                timerRunning = false;
                CancelInvoke(nameof(UpdateTimer));

                // �ð��� �� �Ǿ��� ��
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

        // Ÿ�̸� UI ������Ʈ
        UpdateTimerUI();
    }

    // Ÿ�̸� UI�� �����ϴ� �Լ�
    private void UpdateTimerUI()
    {
        string timeText = string.Format("{0}:{1:00}", minutes, seconds);
        timerText.text = timeText;
    }

    // �ð��� �� �Ǿ��� �� ȣ��Ǵ� �Լ�
    private void TimeUp()
    {
        // �ð� ���� �α� ���
        Debug.Log("�ð� ����");
    }
}