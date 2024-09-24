using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoUIController : MonoBehaviour
{
    public RawImage rawImage;  // Raw Image�� ���� ���� ���
    public VideoPlayer videoPlayer;  // Video Player ������Ʈ

    void Start()
    {
        // ���� �÷��̾� �غ� �Ϸ� �� �̺�Ʈ ���
        videoPlayer.prepareCompleted += OnVideoPrepared;

        // ���� �غ� �� ���
        PrepareVideo();
    }

    void PrepareVideo()
    {
        // ���� �÷��̾� �غ� ����
        videoPlayer.Prepare();
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        // ���� �÷��̾ �غ�Ǿ��� �� ������ ����ϰ� Raw Image�� �ؽ�ó ����
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }

    void Update()
    {
        // ������ ��� ���̰�, �ؽ�ó�� �������� �ʾҴٸ� ����
        if (videoPlayer.isPrepared && rawImage.texture == null)
        {
            rawImage.texture = videoPlayer.texture;
        }
    }
}
