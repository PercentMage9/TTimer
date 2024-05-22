using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class TimeInputField : MonoBehaviour
{
    private TMP_InputField inputField;
    private Timer timer;
    private const int MaxTimeInSeconds = 3599; // 59:59

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        timer = FindObjectOfType<Timer>();
        inputField.onEndEdit.AddListener(FormatInput);
    }

    private void FormatInput(string input)
    {
        string formattedInput = ConvertToTimeFormat(input);
        inputField.text = formattedInput;
        timer.StartTimer(formattedInput);
    }

    private string ConvertToTimeFormat(string input)
    {
        string numericInput = Regex.Replace(input, @"[^0-9]", "");

        if (numericInput.Length == 0)
        {
            return "00:00";
        }

        int totalTimeInSeconds;

        if (numericInput.Length <= 2)
        {
            totalTimeInSeconds = int.Parse(numericInput);
        }
        else if (numericInput.Length <= 4)
        {
            int seconds = int.Parse(numericInput.Substring(numericInput.Length - 2));
            int minutes = int.Parse(numericInput.Substring(0, numericInput.Length - 2));
            totalTimeInSeconds = minutes * 60 + seconds;
        }
        else
        {
            int seconds = int.Parse(numericInput.Substring(numericInput.Length - 2));
            int minutes = int.Parse(numericInput.Substring(numericInput.Length - 4, 2));
            totalTimeInSeconds = minutes * 60 + seconds;
        }

        totalTimeInSeconds = Mathf.Min(totalTimeInSeconds, MaxTimeInSeconds);

        int finalMinutes = totalTimeInSeconds / 60;
        int finalSeconds = totalTimeInSeconds % 60;
        return string.Format("{0:00}:{1:00}", finalMinutes, finalSeconds);
    }
}