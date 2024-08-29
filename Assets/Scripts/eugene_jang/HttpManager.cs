using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;  // http ����� ���� ���� �����̽�
using System.Text;      // json, csv ���� ���� ������ ���ڵ�  (UTF-8)�� ���� ���� �����̽�
using UnityEngine.UI;
using System;


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

    // �̹��� ������ Get���� �޴� �Լ�
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
    
    public void GetJson()
    {

    }

    IEnumerator GetJsonImageRequest(string url)
    {
        // url�� ���� Get���� ��û�� �غ��Ѵ�. 
        UnityWebRequest request = UnityWebRequest.Get(url);
        // �غ�� ��û�� ������ �����ϰ� ������ �ö����� ��ٸ���.
        yield return request.SendWebRequest();
        // ����, ������ �����̶��...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // �ؽ�Ʈ�� �޴´�.
            string result = request.downloadHandler.text;
            
            //������� json �����͸� RequestImage ����ü ���·� �Ľ��Ѵ�.
            RequestImage reqImageData = JsonUtility.FromJson<RequestImage>(result);

            byte[] binaries = Encoding.UTF8.GetBytes(reqImageData.img);
            byte[] imageBytes = Convert.FromBase64String(reqImageData.img);

            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(imageBytes))
            {
                img_response.texture = texture;

                text_response.text = "�̹��� �ε� ����";
            }
            else
            {
                //�̹��� �ε� ���� �� ���� �޽��� ���
                text_response.text = "�̹��� �ε� ����";
            }

            //if(binaries.Length >0)
            //{
            //    Texture2D texture = new Texture2D(2, 2);
                
            //    // byte �迭�� �� raw �����͸� �ؽ��� ���·� ��ȯ�ؼ� texture2D �ν��Ͻ��� ��ȯ�Ѵ�.
            //    texture.LoadRawTextureData(binaries);
            //    texture.EncodeToJPG();

            //    img_response.texture = texture;
                
            //}

        }
        // �׷��� �ʴٸ�...
        else
        {
        // ���� ������ text_response�� �����Ѵ�.
        text_response.text = request.responseCode + " : " + request.error;
            Debug.LogError(request.responseCode + " : " + request.error);
        }

    }
}

[System.Serializable]
public struct RequestImage
{
    public string img;
}
