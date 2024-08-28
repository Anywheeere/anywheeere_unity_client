using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;  // http ����� ���� ���� �����̽�
using System.Text;      // json, csv ���� ���� ������ ���ڵ�  (UTF-8)�� ���� ���� �����̽�

public class HttpManager : MonoBehaviour
{
    public string url;

    void Start()
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
        }
        // �׷��� �ʴٸ�...(400,404 etc)
        else
        {
            // ���� ������ ����Ѵ�.
            print(request.error);
        }



    }

}
