using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musicTracks;
    public Slider volumeSlider;

    private int currentTrackIndex = -1;

    public Timer timerScript;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volumeSlider.value = 0.25f;
        audioSource.volume = volumeSlider.value;

        volumeSlider.onValueChanged.AddListener(UpdateVolume);

        PlayRandomTrack();
    }

    private void Update()
    {
        if (timerScript.isRunning == false)
        {
            audioSource.Pause();
        }
        else if (!audioSource.isPlaying)
        {
            PlayRandomTrack();
        }
    }

    public void PlayRandomTrack()
    {
        if (musicTracks.Length > 0)
        {
            currentTrackIndex = Random.Range(0, musicTracks.Length);
            audioSource.clip = musicTracks[currentTrackIndex];
            audioSource.Play();
        }
    }

    public void UpdateVolume(float volume)
    {
        audioSource.volume = volume;
    }
}