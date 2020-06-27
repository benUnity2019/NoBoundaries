using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMPro.TextMeshProUGUI text;

    [Header("Settings")]
    [SerializeField] string label;

    [Header("Data")]
    [SerializeField] int value;

    public int Value
    {
        get => value;
        set
        {
            value = Mathf.Clamp(value, 0, int.MaxValue);
            this.value = value;
            text.text = label;
            string str = label;
            if (str == "")
                text.text = value.ToString();
            else
                text.text = str.Replace("\\v", value.ToString());
        }
    }
}