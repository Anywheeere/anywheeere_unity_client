using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
            print("NYC");
            //CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(8);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            print("Rome");
            //CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(9);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            print("Paris");
            //CesiumMap.GetComponent<CesiumSamplesFlyToLocationHandler>().FlyToLocation(10);
        }

    }
}
