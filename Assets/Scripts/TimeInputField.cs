using UnityEngine;
using TMPro;

public class TimeInputField : MonoBehaviour
{
    private TMP_InputField inputField;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(FormatInput);
    }

    private void FormatInput(string input)
    {
        string pattern = @"^([0-5][0-9]):([0-5][0-9])$";
        if (System.Text.RegularExpressions.Regex.IsMatch(input, pattern))
        {
            inputField.text = input;
        }
        else
        {
            inputField.text = "00:00";
        }
    }
}