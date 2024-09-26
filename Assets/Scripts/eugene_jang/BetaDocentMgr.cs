using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static DocentManager;

public class BetaDocentMgr : MonoBehaviour
{
    public GameObject CesiumMap;

    public string url;

    public AudioSource audioSource;
    public Text docentText;

    public GameObject bgmBox;
    public GameObject docentBox;

    public GameObject mapPanel;

    public int idx;
    string jsonData;

    public GameObject CountryPanel;
    public GameObject USAPanel;
    public GameObject ItalyPanel;
    public GameObject JapanPanel;

    public GameObject NYCPanel;
    public GameObject SFPanel;
    public GameObject LVPanel;

    public GameObject RomePanel;
    public GameObject VenicePanel;

    public GameObject TokyoPanel;
    public GameObject KyotoPanel;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapPanel.activeInHierarchy)
            {
                mapPanel.SetActive(false);
                OnDisable();
            }
            else if (!mapPanel.activeInHierarchy)
            {
                mapPanel.SetActive(true);
            }

        }


    }
    private void OnDisable()
    {
        CountryPanel.SetActive(true);
        USAPanel.SetActive(false);
        ItalyPanel.SetActive(false);
        JapanPanel.SetActive(false);
        NYCPanel.SetActive(false);
        SFPanel.SetActive(false);
        LVPanel.SetActive(false);
        RomePanel.SetActive(false);
        VenicePanel.SetActive(false);
        TokyoPanel.SetActive(false);
        KyotoPanel.SetActive(false);
    }

    public void CountryBtnOnClick(string country)
    {
        CountryPanel.SetActive(false);
        if      (country == "USA")   { idx = 0; USAPanel.SetActive(true); }
        else if (country == "Italy") { idx = 1; ItalyPanel.SetActive(true);  }
        else if (country == "Japan") { idx = 2; JapanPanel.SetActive(true);  }

        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
        // ���� BGM �� ������ Audio ���
    }

    public void SelectCity(string city)
    {
        if      (city == "NewYork")         { idx = 3; NYCPanel.SetActive(true); }
        else if (city == "San Francisco")   { idx = 4; SFPanel.SetActive(true); }
        else if (city == "Las Vegas")       { idx = 5; LVPanel.SetActive(true); }
        USAPanel.SetActive(false);

        if      (city == "Rome")   { idx = 6;  RomePanel.SetActive(true); }
        else if (city == "Venice") { idx = 7; VenicePanel.SetActive(true); }
        ItalyPanel.SetActive(false);

        if      (city == "Kyoto") { idx = 8; KyotoPanel.SetActive(true); }
        else if (city == "Tokyo") { idx = 9; TokyoPanel.SetActive(true); }
        JapanPanel.SetActive(false);

        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
        // ���� BGM �� ������ Audio ���
    }

    public void SelectMonument(string monument)
    {
        print(monument);
        if      (monument == "������ ���Ż�") { idx = 10; }
        else if (monument == "Ÿ�� ������") { idx = 11; }
        else if (monument == "��Ʈ�� ��ũ") { idx = 12; }
        else if (monument == "���Ŭ�� �긴��") { idx = 13; }
        else if (monument == "��� ����Ʈ �긴��") { idx = 14; }
        else if (monument == "�丮 ����") { idx = 15; }
        else if (monument == "�������ý��� �غ�") { idx = 16; }
        else if (monument == "�� ���Ǿ�") { idx = 17; }
        else if (monument == "�������� �м�") { idx = 18; }
        else if (monument == "������ �縮��") { idx = 19; }
        else if (monument == "����ġ�� ȣ��") { idx = 20; }
        else if (monument == "���׿� ����") { idx = 21; }
        else if (monument == "�ݷμ���") { idx = 22; }
        else if (monument == "�� ����� �뼺��") { idx = 23; }
        else if (monument == "�� ������ �뼺��") { idx = 24; }
        else if (monument == "������ �ٸ�") { idx = 25; }
        else if (monument == "���Ͻ� �����") { idx = 26; }
        else if (monument == "�ݰ���") { idx = 27; }
        else if (monument == "�߻�ī�� ž") { idx = 28; }
        else if (monument == "ŰŸ�� �ٸ���") { idx = 29; }
        else if (monument == "����Ÿ��") { idx = 30; }
        else if (monument == "���� ��ī�� Ʈ��") { idx = 31; }
        else if (monument == "����� ����") { idx = 32; }

        // ���帶ũ Audio ��� ( BGM ���� ����. ���� BGM ��� �÷���)
        GetDocent(monument);
        GetAudioDocent();
        CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(idx);
    }

    public void GetDocent(string target)
    {
        jsonData = "{\"text\":\"" + target + "\"}";
        print(idx);
        StartCoroutine(GetDocentFromAI(url, jsonData));
    }

    IEnumerator GetDocentFromAI(string url, string jsonData)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + "docent"))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            DocentResponse responseData = JsonUtility.FromJson<DocentResponse>(www.downloadHandler.text);
            docentText.text = responseData.docent;
        }
    }

    public void GetAudioDocent()
    {
        StartCoroutine(GetAudioDocentFromAI());
    }

    IEnumerator GetAudioDocentFromAI()
    {
        string jsonData = "{\"path\":\"./output.wav\"}";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip( url + "audio", AudioType.WAV))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                DownloadHandlerAudioClip downloadHandler = www.downloadHandler as DownloadHandlerAudioClip;
                PlayAudioClip(downloadHandler.audioClip);
            }
        }
    }

    void PlayAudioClip(AudioClip clip)
    {

        //if (audioSource != null)
        //{
        //    AudioSource[] audioSources = GetComponents<AudioSource>();
        //    foreach (AudioSource audioSource in audioSources)
        //    {
        //        Destroy(audioSource);
        //    }
        //    print("�� �ڿ�");
        //}

        // ���� �͵��� ����� 
        // �ʿ��� ����� ���
    }



}
