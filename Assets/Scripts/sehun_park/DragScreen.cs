using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScreen : MonoBehaviour
{

    private Vector3 dragStartPoint;
    private Vector3 dragStartPosition;
    private bool isDragging = false;

    public float moveSpeed = 5;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư�� ���� ���
        {
            print("���� ������");
            StartDrag();
        }

        if (Input.GetMouseButton(0) && isDragging) // �巡�� ���� ��
        {
            print("�巡�� ��");
            Drag();
        }

        if (Input.GetMouseButtonUp(0)) // ���콺 ��ư�� �� ���
        {
            print("�巡�� ��");
            isDragging = false;
        }
    }

    void StartDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // ������Ʈ�� ��ġ�� �������� �巡�� ���� ��ġ�� ����մϴ�.
        dragStartPoint = ray.GetPoint(CalculateDistanceToDragPlane(ray)); // �巡�� ���� ��ġ
        dragStartPosition = transform.position; // �巡�� ���� �� ������Ʈ�� ��ġ
        isDragging = true;
    }

    void Drag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // ���� ���콺 ��ġ�� �������� �巡�� ��ġ�� ����մϴ�.
        Vector3 dragCurrentPoint = ray.GetPoint(CalculateDistanceToDragPlane(ray));
        Vector3 difference = dragCurrentPoint - dragStartPoint;
        print(difference);

        // ������Ʈ�� �� ��ġ�� ����Ͽ� �̵�
        transform.position = dragStartPosition + difference * moveSpeed;

        // �巡�� ���� ������ ������Ʈ�Ͽ� �������� �̵� ����
        dragStartPoint = dragCurrentPoint;

        print("dragStartPoint" + dragStartPoint);
        print("dragCurrentPoint" + dragCurrentPoint);
    }

    // �巡�װ� ������ �������� �Ÿ��� ����մϴ�.
    float CalculateDistanceToDragPlane(Ray ray)
    {
        // �巡�׸� ���� ����� �����մϴ�. (���⼭�� ������ Y=0���� ����)
        Plane dragPlane = new Plane(Vector3.up, Vector3.zero);
        float enter;
        if (dragPlane.Raycast(ray, out enter))
        {
            return enter;
        }
        return 0;
    }
}