using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public TextMeshProUGUI ClickTotalText;

    public float TotalClicks;

    public static Manager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddClicks()
    {
        TotalClicks++;
        if(FindObjectOfType<Bonus>() != null)
        {
            FindObjectOfType<Bonus>().Clicked();
        }

        ClickTotalText.text = TotalClicks.ToString("0");
    }
}
