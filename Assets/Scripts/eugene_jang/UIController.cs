using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


[Serializable]
enum PlayStates
{
    map,
    region,
    landmark
}

public class UIController : MonoBehaviour
{
    public GameObject CesiumMap;
    public SkyboxManager skyboxManager;

    public string url;
    // ���õ� ���÷� �̵��Ѵ�.
    // ���ÿ� ����Ʈ �����͸� AI ���Լ� �޾ƿ´�.
    // �޾ƿ� �����͸� �����صд�. text / audio

    public string docent;

    public string audioAddress;
    public AudioClip docentAudio;

    string jsonData;

    public GameObject docentUI;
    public Text docentText;
    // UI�� Ȱ��ȭ �Ǿ������� ���� 
    void Start()
    {
        // ������ UI�� Ȱ��ȭ
        // map mode ����
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("NYC");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(1);
            // �������� �̵� �� ���� 
            jsonData = "{\"text\":\"�����ǿ��Ż�\"}";
            GetDocent();
            
            docentText.text = "������ ���Ż� ����Ʈ ����";
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("Statue of Liberty");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(2);
        }

        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("Rome");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(4);
            jsonData = "{\"text\":\"�ݷμ���\"}";
            GetDocent();

            docentText.text = "�ݷμ��� ����Ʈ ����";
        }

        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("Colosseum");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(5);
        }

        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }


            print("Paris");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(7);
            jsonData = "{\"text\":\"����ž\"}";
            GetDocent();

            docentText.text = "����ž ����Ʈ ����";
        }

        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            if (skyboxManager != null)
            {
                skyboxManager.SetSkybox(0); // Call SetSkybox from SkyboxManager
            }

            print("Eiffel Tower");
            CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(8);
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            if (docentUI.activeInHierarchy)
            {
                docentUI.SetActive(false);
            }
            else if (!docentUI.activeInHierarchy)
            {
                docentUI.SetActive(true);
            }
        }

    }
    // ������� �Լ��� ��

    #region ������� ��� �Լ���
    public void GetDocent()
    {
        StartCoroutine(GetDocentFromAI(url, jsonData));
    }

    // �̵��ϴ� ���� ������ �����ϰ� docent �ؽ�Ʈ�� audio �ּҸ� �޾� ��
    IEnumerator GetDocentFromAI(string url, string jsonData)
    {   
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            docentText.text = www.downloadHandler.text;
            //�޾ƿ� �ؽ�Ʈ(json���� ��) ����Ʈ �κ��� string ������ ���� �� ��

            // �޾ƿ� json���� docent �κа� audio �ּ� �κ��� �и��� �־� ��
            // ����� ����Ʈ �޴� �Լ� ����
            // docentAudio�� �޾ƿ� AudioClip ���� 
        }
    }

    // docent audio �� �޾Ƽ� AudioClip�� ���� �ϴ� �Լ�
    IEnumerator GetDecentAudioFromAI(string url)
    {
        jsonData = "{\"path\":\"./result.wav\"}";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            DownloadHandlerAudioClip downloadHandler = www.downloadHandler as DownloadHandlerAudioClip;
            docentAudio = downloadHandler.audioClip;

            // docentAudio�� ���� �ؾ� �� �� ���� �Ѵ�.(��� �־� ������~?)
        }
    }





    // ����ȯ�濡�� �޾ƿ� docent�� UI�� ���� ��Ų��.( Audio�� ���� ���� // ����� ���� ��ư�� ?)
    void ViewDocent()
    {

    }


    #endregion


}
