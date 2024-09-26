using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMMgr : MonoBehaviour
{

    public AudioSource audioSource;

    public int preIdx;

    public AudioClip[] audios = new AudioClip[33];
    void Start()
    {

    }

    void Update()
    {
        int curIdx = BetaDocentMgr.Instance.idx;

        if (preIdx != curIdx)
        {
            preIdx = curIdx;
            if( curIdx < 10)
            {
                PlayFixedAudio(audios, curIdx);

            }
        }
    }
    // idx�� Audio resource�� null �� �ƴ϶�� 
    // Audio�� ���
    // bgm ���� �򸮰�.. 
    // ����Ʈ ���.. 

    public void PlayFixedAudio(AudioClip[] clips, int idx)
    {
        Debug.Log("if �� ����");
        if (audioSource != null)
        {
            AudioSource[] audioSources = GetComponents<AudioSource>();
            Debug.Log("destroy �Ǳ� ����...�αٵα�");
            foreach (AudioSource audioSource in audioSources)
            {
                Destroy(audioSource);
            }
            print("�� �ڿ�");
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clips[idx];
        audioSource.volume = 0.3f;
        audioSource.Play();
    }

}
