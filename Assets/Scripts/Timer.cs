using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_InputField timeInputField;
    public Button startButton;
    public TextMeshProUGUI countdownText;
    public Slider timerSlider;

    private float currentTime;
    private float totalTime;
    private bool isRunning;

    private void Start()
    {
        startButton.onClick.AddListener(StartTimer);
        timerSlider.onValueChanged.AddListener(UpdateCurrentTime);
        ResetTimer();
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
                }
                else
                {
                    Debug.LogError("Time input must be greater than 00:00");
                    timerSlider.interactable = false;
                }
            }
            else
            {
                Debug.LogError("Invalid input in input field");
                timerSlider.interactable = false;
            }
        }
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
    }
}