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

    public List<InputField> userInputs = new List<InputField>();
    public Toggle freeUser;

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

    // ������ Json �����͸� Post�ϴ� �Լ�
    public void PostJson()
    {
        StartCoroutine(PostJsonRequest(url));
    }
    IEnumerator PostJsonRequest(string url)
    {
        // ������� �Է� ������ Json �����ͷ� ��ȯ�ϱ�
        JoinUserData userData = new JoinUserData();
        userData.id = Convert.ToInt32(userInputs[0].text);
        userData.password = userInputs[1].text;
        userData.nickName = userInputs[2].text;
        userData.freeAccount = freeUser.isOn;
        string userJsonData = JsonUtility.ToJson(userData, true);
        byte[] jsonBins = Encoding.UTF8.GetBytes(userJsonData);

        // POST�� �ϱ� ���� �غ� �Ѵ�.
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(jsonBins);
        request.downloadHandler = new DownloadHandlerBuffer();

        // ������ Post�� �����ϰ� ������ �� �� ���� ��ٸ���.
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            // �ٿ�ε� �ڵ鷯���� �ؽ�Ʈ ���� �޾Ƽ� UI�� ����Ѵ�.
            string response = request.downloadHandler.text;
            text_response.text = response;
            Debug.LogError(text_response.text);
        }
        else
        {
            text_response.text = request.error;
            Debug.LogError(request.error);
        }
    }

    public void PostJsonToAI()
    {
        StartCoroutine(PostPointJsonRequest(url));
    }
    IEnumerator PostPointJsonRequest(string url)
    {
        PointedPlace testData = new PointedPlace(40.6893f, -74.0448f, "NewYork", "StatueOfLiberty" );
        string pointJsonData = JsonUtility.ToJson(testData, true);
        byte[] jsonBins = Encoding.UTF8.GetBytes(pointJsonData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        //request.uploadHandler = new UploadHandlerRaw(jsonBins);
        request.uploadHandler = new UploadHandlerRaw(jsonBins);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // �ٿ�ε� �ڵ鷯���� �ؽ�Ʈ ���� �޾Ƽ� UI�� ����Ѵ�.
            string response = request.downloadHandler.text;
            text_response.text = response;
            Debug.LogError(text_response.text);
        }
        else
        {
            text_response.text = request.error;
            Debug.LogError(request.error);
        }

    }
}



[System.Serializable]
public struct RequestImage
{
    public string img;
}

[System.Serializable]
public struct JoinUserData
{
    public int id;
    public string password;
    public string nickName;
    public bool freeAccount;
}
