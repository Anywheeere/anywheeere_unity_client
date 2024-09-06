using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class DocentManager : MonoBehaviour
{
    public string url;
    // ���õ� ���÷� �̵��Ѵ�.
    // ���ÿ� ����Ʈ �����͸� AI ���Լ� �޾ƿ´�.
    // �޾ƿ� �����͸� �����صд�. text / audio

    public string docent;

    public string audioAddress;
    public AudioClip docentAudio;

    // ����Ʈ�� �����ϸ� ����Ǿ� �ִ� text�� UI ���·� ǥ���Ѵ�.
    // audio�� AudioClip ���� ���� �Ѵ�.


    void Start()
    {
            
    }

    void Update()
    {
        
    }

    public void GetDocent()
    {
        StartCoroutine(GetDocentFromAI(url));
    }

    // �̵��ϴ� ���� ������ �����ϰ� docent �ؽ�Ʈ�� audio �ּҸ� �޾� ��
    IEnumerator GetDocentFromAI(string url)
    {
        string jsonData = "{\"text\":\"�����ǿ��Ż�\"}";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonByte);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            docent = www.downloadHandler.text;
            //�޾ƿ� �ؽ�Ʈ(json���� ��) ����Ʈ �κ��� string ������ ���� �� ��

            // �޾ƿ� json���� docent �κа� audio �ּ� �κ��� �и��� �־� ��
            // ����� ����Ʈ �޴� �Լ� ����
            // docentAudio�� �޾ƿ� AudioClip ���� 
        }
    }

    // docent audio �� �޾Ƽ� AudioClip�� ���� �ϴ� �Լ�
    IEnumerator GetDecentAudioFromAI(string url)
    {
        string jsonData = "{\"path\":\"./result.wav\"}";
        using(UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
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

    

}
