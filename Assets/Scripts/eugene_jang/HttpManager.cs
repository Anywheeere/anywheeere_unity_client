using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;  // http ����� ���� ���� �����̽�
using System.Text;      // json, csv ���� ���� ������ ���ڵ�  (UTF-8)�� ���� ���� �����̽�
using UnityEngine.UI;


public class HttpManager : MonoBehaviour
{
    public string url;
    public Text text_response;
    public RawImage img_response;

    public void Get()
    {
        StartCoroutine(GetRequest(url));
    }

    // Get ��� �ڷ�ƾ �Լ�
    IEnumerator GetRequest(string url)
    {
        // http Get ��� �غ� �Ѵ�.
        UnityWebRequest request = UnityWebRequest.Get(url);

        // ������ Get ��û�� �ϰ�, ������ ���� ������ �� �� ���� ����Ѵ�. 
        yield return request.SendWebRequest();

        // ����, �����κ��� �� ������ ����(200)�̶��...
        if(request.result == UnityWebRequest.Result.Success)
        {
            // ������� �����͸� ����Ѵ�.
            string response = request.downloadHandler.text;
            print(response);
            text_response.text = response;

        }
        // �׷��� �ʴٸ�...(400,404 etc)
        else
        {
            // ���� ������ ����Ѵ�.
            print(request.error);
            text_response.text = request.error;
        }


    }
    public void GetImage()
    {
        StartCoroutine(GetImageRequest(url));
    }

    IEnumerator GetImageRequest(string url)
    {
        // get(Texture) ����� �غ��Ѵ�.
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        // ������ ��û�� �ϰ� , ������ ���� ������ ��ٸ���.
        yield return request.SendWebRequest();

        // ����, ������ �����̶��...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // ���� �ؽ��� �����͸� Textur2D ������ �޾� ���´�.
            Texture2D response = DownloadHandlerTexture.GetContent(request);
            // Texture2D �̹����� img_response�� texture ������ �־�д�.
            img_response.texture = response;

            // text_response�� ���� �ڵ� ��ȣ�� ����Ѵ�.
            text_response.text = "���� - " + request.responseCode.ToString();

        }else
        {
            print(request.error);
            text_response.text = request.error;
        }
    }

}
