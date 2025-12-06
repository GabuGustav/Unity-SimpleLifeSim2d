using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio")]
    public AudioSource audioSource;
    public List<AudioClip> tracks = new List<AudioClip>();
    public AudioMixer mixer;

    private int lastTrack = -1;

    private const string volumeKey = "MusicVolumeSlider";
    private const string volumeParam = "MusicVolume";

    private void Awake()
    {
        // Singleton + persist
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Ensure the prefab has its own AudioSource
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.loop = false;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Load saved volume
        float saved = PlayerPrefs.GetFloat(volumeKey, 1f);
        ApplyMixerVolume(saved);

        // Ensure volume is applied safely after the first frame
        StartCoroutine(ApplyNextFrame(saved));

        // Start the music loop
        StartCoroutine(MusicLoop());
    }

    private IEnumerator MusicLoop()
    {
        while (true)
        {
            PlayRandomTrack();
            // Wait until track finishes
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }

    private void PlayRandomTrack()
    {
        if (tracks.Count == 0) return;

        int index;
        do
        {
            index = Random.Range(0, tracks.Count);
        }
        while (index == lastTrack && tracks.Count > 1);

        lastTrack = index;
        audioSource.clip = tracks[index];
        audioSource.Play();
    }

    // Called by slider or manually
    public void SetVolumeFromSlider(float sliderValue)
    {
        sliderValue = Mathf.Clamp(sliderValue, 0.0001f, 1f);

        // Save preference
        PlayerPrefs.SetFloat(volumeKey, sliderValue);
        PlayerPrefs.Save();

        // Apply immediately
        ApplyMixerVolume(sliderValue);

        // Safety: reapply next frame
        StartCoroutine(ApplyNextFrame(sliderValue));
    }

    private void ApplyMixerVolume(float sliderValue)
    {
        if (mixer != null)
        {
            float dB = Mathf.Log10(sliderValue) * 20f;
            mixer.SetFloat(volumeParam, dB);
        }
    }

    private IEnumerator ApplyNextFrame(float sliderValue)
    {
        yield return null;
        ApplyMixerVolume(sliderValue);
    }

    public float GetSavedSliderValue()
    {
        return PlayerPrefs.GetFloat(volumeKey, 1f);
    }
}
