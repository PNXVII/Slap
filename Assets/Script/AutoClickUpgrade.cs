using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoClickUpgrade : MonoBehaviour
{  
    public int autoClicksPerSec;
    public int minimumClickToUnlock;

    private Manager manager;
    public TextMeshProUGUI priceText, amountText;

    private void Start()
    {
        manager = Manager.instance; ;
        UpdateText();
    }

    public void BuyUpgrade()
    {
        if (manager.TotalClicks >= minimumClickToUnlock)
        {
            manager.TotalClicks -= minimumClickToUnlock;

            autoClicksPerSec++;
            minimumClickToUnlock *= 2;

            UpdateText();
        }
    }

    private void Update()
    {
        if (autoClicksPerSec > 0)
        {
            manager.TotalClicks += autoClicksPerSec * Time.deltaTime;
             
            manager.ClickTotalText.text = manager.TotalClicks.ToString("0");
        }
    }

    private void UpdateText()
    {
        priceText.text = "Need" + minimumClickToUnlock.ToString("0");
        amountText.text = "+" + (autoClicksPerSec + 1).ToString() + "/s";
    }
}
