using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgSound;
    public AudioClip[] bgList;
    public AudioMixer mixer;
    
    public static SoundManager instance;
    private void Awake()
    {
        BackgroundSoundPlay(bgList[0]);
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BGSoundVolume(float val)
    {
        mixer.SetFloat("bgSound",Mathf.Log10(val)*20);
    }
    public void SFXSoundVolume(float val)
    {
        mixer.SetFloat("SFX",Mathf.Log10(val)*20);
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName+"Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip=clip;
        audiosource.volume=0.1f;
        audiosource.outputAudioMixerGroup=mixer.FindMatchingGroups("SFX")[0];
        
        audiosource.Play();

        Destroy(go,clip.length);
    }
    public void BackgroundSoundPlay(AudioClip clip)
    {
        bgSound.outputAudioMixerGroup=mixer.FindMatchingGroups("bgSound")[0];
        bgSound.clip = clip;
        bgSound.loop=true;
        bgSound.volume=0.1f;
        bgSound.Play();
    }
}
