using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioClip[] audioClips;
    AudioClip moveClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource PlayPlayerSound(int index)
    {
        if (audioClips != null)
        {
            GameObject audioPlayer = new GameObject("AudioPlayer");
            audioPlayer.transform.position = transform.position;
            AudioSource audioSource = audioPlayer.AddComponent<AudioSource>();
            audioSource.clip = audioClips[index];
            audioSource.Play();
            return audioSource;
        }
        return null;
    }

    public void PlayPlayerSoundMove(int index, float audioStereoPan)
    {
        if (moveClip != null && audioClips != null)
        {
            GameObject audioPlayer = new GameObject("AudioPlayer");
            audioPlayer.transform.position = transform.position;
            AudioSource audioSource = audioPlayer.AddComponent<AudioSource>();
            audioSource.clip = audioClips[index];
            moveClip = audioSource.clip;
            audioSource.panStereo = audioStereoPan;
        }
    }
}
