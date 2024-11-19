using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //System
    public static SoundManager instance;
    public float VolumeWhole = 1.0f;
    public float VolumeBgm = 1.0f;

    //오디오소스 풀링
    public AudioSource[] audioPool;


    public float[] soundsVolume;


    private void Awake()
    {
        instance = this;
        audioPool = new AudioSource[50];
    }

    public void ChangeVolume(float volume, int type) //type = 1 Bgm / type = 0 SFX
    {
        soundsVolume[type] = volume;
    }

    private IEnumerator DestroyAudio(GameObject audioGO, float clipLength)
    {
        Debug.Log("Destroy after : " + clipLength);
        yield return new WaitForSeconds(clipLength);
        Destroy(audioGO);
    }
    public void PlaySFX(AudioClip _clip, Transform playTransform)
    {
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                //여기서 play 안되는듯
                GameObject go = new GameObject { name = _clip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = playTransform.position;
                audioPool[i].PlayOneShot(_clip, soundsVolume[0]);

                StartCoroutine(DestroyAudio(go, _clip.length * 3.3f));
                return;
            }
        }
        Debug.Log("AuioSource is Full");
        return;
    }
}
