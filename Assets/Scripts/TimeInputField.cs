using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class TimeInputField : MonoBehaviour
{
    private TMP_InputField inputField;
    private Timer timer;

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
        else if (numericInput.Length <= 2)
        {
            int seconds = int.Parse(numericInput);
            return string.Format("{0:00}:{1:00}", 0, seconds);
        }
        else if (numericInput.Length <= 4)
        {
            int seconds = int.Parse(numericInput.Substring(numericInput.Length - 2));
            int minutes = int.Parse(numericInput.Substring(0, numericInput.Length - 2));
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            return "00:00";
        }
    }
}