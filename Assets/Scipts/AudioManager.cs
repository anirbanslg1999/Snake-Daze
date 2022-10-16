using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource BackGroundSource;
    [SerializeField] AudioSource ButtonSource;
    private bool onMute = false;
    // Making a array of the class Sounds so we can get sound clips for different sound types. 
    [SerializeField] Sounds[] sounds;

    // Making Instance of the class.
    private static AudioManager instance;
    public static AudioManager Instance
    { get 
        { 
            return instance; 
        } 
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            BackGroundSource.Play();
        }
        else{ Destroy(gameObject); }
    }

    public void PlayBackGroundSound(SoundTypes _soundType)
    {
        if (onMute)
        {
            BackGroundSource.volume = 0f;
            return;
        }
        BackGroundSource.volume = 1f;
        AudioClip clip = getAudioClip(_soundType);
        if (clip != null)
        {
            BackGroundSource.clip = clip;
            BackGroundSource.Play();
        }
        else
        {
            Debug.LogWarning("Clip was Found Null Check the source " + _soundType);
        }
    }

    public void PlayEffectSound(SoundTypes _soundType)
    {
        if (onMute) return;
        AudioClip clip = getAudioClip(_soundType);
        if(clip != null)
        {
            ButtonSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Clip was Found Null Check the source " + _soundType);
        }
    }
    private AudioClip getAudioClip(SoundTypes _ST)
    {
        Sounds item = Array.Find(sounds, s => s.soundType == _ST);
        if (item != null)
        {
            return item.audioClip;
        }
        else return null;
    }

}
// Defining the Sounds by making a class. It also works like struc, same thing. 
[Serializable]
public class Sounds
{
    public SoundTypes soundType;
    public AudioClip audioClip;
}
public enum SoundTypes
{
    StartMenuBG,
    GameBG,
    ButtonPressed,
    Collectable,
    GameOver
}
