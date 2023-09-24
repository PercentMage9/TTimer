using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public Button toggleButton; // The same button for play/pause toggle
    public Slider volumeSlider; // Slider for volume control
    public AudioClip[] musicTracks; // Array of music tracks to play

    private bool isPlaying = false; // Flag to track the playing state

    private void Start()
    {
        TogglePlayPause(); // Toggle play/pause state when initializing
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    public void Initialize()
    {
        // Initialize the volume slider value to the initial volume
        volumeSlider.value = audioSource.volume;
    }

    public void TogglePlayPause()
    {
        isPlaying = !isPlaying;

        if (isPlaying)
        {
            if (musicTracks.Length > 0)
            {
                int randomIndex = Random.Range(0, musicTracks.Length);
                audioSource.clip = musicTracks[randomIndex];
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("No music tracks are assigned.");
            }
        }
        else
        {
            audioSource.Pause();
        }

        // Update the button text or image as needed for play/pause
        // Example code assuming you have a Text component for button text:
        // toggleButton.GetComponentInChildren<Text>().text = isPlaying ? "Pause" : "Play";
    }

    public void UpdateVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
