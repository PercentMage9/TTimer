using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musicTracks;
    public Slider volumeSlider;
    public TextMeshProUGUI countdownText;

    private int currentTrackIndex = -1;
    private bool isPaused = false;

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
        if (isPaused)
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

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
    }

    // Add the PlayMusic and PauseMusic methods
    public void PlayMusic()
    {
        isPaused = false;
        audioSource.Play();
    }

    public void PauseMusic()
    {
        isPaused = true;
        audioSource.Pause();
    }
}
