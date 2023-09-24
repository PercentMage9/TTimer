using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_InputField timeInputField;
    public Button startButton;
    public Button pausePlayButton;
    public Button resetButton;
    public TextMeshProUGUI countdownText;
    public Slider timerSlider;

    public Sprite playSprite;
    public Sprite pauseSprite;

    private float currentTime;
    private float totalTime;
    public bool isRunning;
    private Image pausePlayImage;

    private void Awake()
    {
        pausePlayImage = pausePlayButton.GetComponent<Image>();
    }

    private void Start()
    {
        startButton.onClick.AddListener(StartTimer);
        pausePlayButton.onClick.AddListener(TogglePausePlay);
        resetButton.onClick.AddListener(ResetTimer);
        timerSlider.onValueChanged.AddListener(UpdateCurrentTime);
    }

    private void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isRunning = false;
                pausePlayButton.interactable = false;
                TogglePausePlay();
            }

            UpdateUI();
        }
    }

    public void StartTimer()
    {
        if (!isRunning)
        {
            string[] timeParts = timeInputField.text.Split(':');
            if (timeParts.Length == 2 && int.TryParse(timeParts[0], out int minutes) && int.TryParse(timeParts[1], out int seconds))
            {
                totalTime = Mathf.Max(0f, minutes * 60 + seconds);
                if (totalTime > 0f)
                {
                    currentTime = totalTime;
                    isRunning = true;
                    timerSlider.interactable = true;
                    pausePlayButton.interactable = true;
                    pausePlayImage.sprite = pauseSprite;
                }
                else
                {
                    Debug.LogError("Time input must be greater than 00:00");
                    timerSlider.interactable = false;
                    pausePlayButton.interactable = false;
                }
            }
            else
            {
                Debug.LogError("Invalid input");
                timerSlider.interactable = false;
                pausePlayButton.interactable = false;
            }
        }
    }

    public void TogglePausePlay()
    {
        if (isRunning)
        {
            isRunning = false;
        }
        else if (currentTime > 0f || currentTime == totalTime)
        {
            isRunning = true;
        }

        pausePlayImage.sprite = isRunning ? pauseSprite : playSprite;
    }

    private void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        float sliderValue = 100f * (1f - (currentTime / totalTime));
        timerSlider.value = sliderValue;
    }

    private void UpdateCurrentTime(float sliderValue)
    {
        currentTime = totalTime * (1f - sliderValue / 100f);
        UpdateUI();

        pausePlayButton.interactable = currentTime > 0f;
    }

    public void ResetTimer()
    {
        timeInputField.text = "";
        countdownText.text = "00:00";
        timerSlider.value = 0f;
        currentTime = 0f;
        totalTime = 0f;
        isRunning = false;
        timerSlider.interactable = false;
        pausePlayButton.interactable = false;
        pausePlayImage.sprite = playSprite;
    }
}