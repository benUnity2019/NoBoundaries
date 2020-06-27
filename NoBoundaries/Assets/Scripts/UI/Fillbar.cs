using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fillbar : MonoBehaviour
{
    [SerializeField] Image fill;
    [SerializeField] Image background;

    [SerializeField] float lerpSpeed;

    float goal;

    public float Value
    {
        get
        {
            return fill.fillAmount;
        }
        set
        {
            goal = value;
        }
    }

    private void Awake()
    {
        goal = fill.fillAmount;
    }

    private void Update()
    {
        fill.fillAmount = Mathf.Lerp(fill.fillAmount, goal, Time.deltaTime * lerpSpeed);
    }
}