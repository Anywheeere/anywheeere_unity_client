using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SphereManager : MonoBehaviour
{
    public Material sphereMaterial;  // ���̴��� ����� ��Ƽ����
    public float duration = 3.0f;    // ���� ������ ��ȭ�ϴ� �ð�
    public List<Texture2D> textures; // �ۺ����� ����� �ؽ�ó ����Ʈ
    private int currentTextureIndex = 0; // ���� �ؽ�ó �ε���

    private bool isIncreasing = true; // ���� 0���� 1�� ���� ������ ����
    private float startValue = 0f;   // ���̴��� �ʱ� Value ��
    private float endValue = 0.6f;     // ���̴��� ��ǥ Value ��
    private float currentTime = 0f;  // �ð��� ����ϴ� Ÿ�̸�

    private bool isRunning = false;  // �ڷ�ƾ ���� ������ ����

    void Start()
    {
        // ù ��° �ؽ�ó�� �⺻ �ؽ�ó�� ����
        if (textures.Count > 0)
        {
            sphereMaterial.SetTexture("_Texture2D", textures[currentTextureIndex]);
        }
    }

    void Update()
    {
        // i Ű�� ������ �� ���� �ؽ�ó�� ����
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeTexture(1);
        }

        // o Ű�� ������ �� ���� �ؽ�ó�� ����
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeTexture(-1);
        }

        // space�ٸ� ������ �� �� ��ȭ ����/����
        if (Input.GetKeyDown(KeyCode.Space) && !isRunning)
        {
            // �ڷ�ƾ�� �����ϰ� ������ ����
            if (isIncreasing)
            {
                StartCoroutine(ChangeShaderValue(0f, 0.6f)); // 0���� 1�� ����
            }
            else
            {
                StartCoroutine(ChangeShaderValue(0.6f, 0f)); // 1���� 0���� ����
            }

            // ������ ����
            isIncreasing = !isIncreasing;
        }
    }

    // �ؽ�ó�� �����ϴ� �Լ�
    void ChangeTexture(int change)
    {
        currentTextureIndex += change;

        // �ε����� ����Ʈ�� ������ ����� �ʵ��� ����
        if (currentTextureIndex >= textures.Count)
        {
            currentTextureIndex = 0; // �������� ������ ù ��°�� ��ȯ
        }
        else if (currentTextureIndex < 0)
        {
            currentTextureIndex = textures.Count - 1; // ù ��°�� ������ ���������� ��ȯ
        }

        // ���ο� �ؽ�ó�� ��Ƽ���� ����
        sphereMaterial.SetTexture("_Texture2D", textures[currentTextureIndex]);
    }

    // ���� ��ȭ�ϴ� �ڷ�ƾ (0���� 1 �Ǵ� 1���� 0����)
    IEnumerator ChangeShaderValue(float start, float end)
    {
        isRunning = true; // �ڷ�ƾ�� ���� ������ ǥ��
        currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newValue = Mathf.Lerp(start, end, currentTime / duration);
            sphereMaterial.SetFloat("_Value", newValue);
            yield return null;
        }

        // ��Ȯ�� ��ǥ ������ ����
        sphereMaterial.SetFloat("_Value", end);

        isRunning = false; // �ڷ�ƾ ���� �� ���� �� ���� ����
    }
}
