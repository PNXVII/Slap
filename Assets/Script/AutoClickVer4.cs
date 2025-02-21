using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoClickVer4 : MonoBehaviour
{
    public int autoClicksPerSec4;
    public int minimumClickToUnlock4;
    public AudioSource upgradeSound;

    private float addClicks;
    private Manager manager;
    public TextMeshProUGUI priceText, amountText;

    private void Start()
    {
        manager = Manager.instance; ;

        if (PlayerPrefs.GetInt("autoClicksPerSec4") != 0)
        {
            autoClicksPerSec4 = PlayerPrefs.GetInt("autoClicksPerSec4");
        }
        if (PlayerPrefs.GetInt("minimumClickToUnlock4") != 0)
        {
            minimumClickToUnlock4 = PlayerPrefs.GetInt("minimumClickToUnlock4");
        }
        UpdateText();
    }

    public void BuyUpgrade()
    {

        if (manager.TotalClicks >= minimumClickToUnlock4)
        {
            manager.TotalClicks -= minimumClickToUnlock4;

            autoClicksPerSec4 += 5;
            minimumClickToUnlock4 *= 3;

            PlayerPrefs.SetInt("autoClicksPerSec4", autoClicksPerSec4);
            PlayerPrefs.SetInt("minimumClickToUnlock4", minimumClickToUnlock4);
            UpdateText();

            if (upgradeSound != null)
            {
                upgradeSound.Play();
            }
        }
    }

    private void Update()
    {
        if (autoClicksPerSec4 > 0)
        {
            addClicks += autoClicksPerSec4 * Time.deltaTime;
            int clicksToAdd = Mathf.FloorToInt(addClicks);

            Debug.Log("addClicks: " + addClicks + " | clicksToAdd: " + clicksToAdd);

            if (clicksToAdd > 0)
            {
                manager.AddClicks(clicksToAdd, true);
                addClicks -= clicksToAdd;

                Debug.Log("เพิ่มคลิก: " + clicksToAdd + " | TotalClicks: " + manager.TotalClicks);
            }

        }
    }

    private void UpdateText()
    {
        string priceTextFormatted;

        if (minimumClickToUnlock4 >= 1000)
        {
            priceTextFormatted = (minimumClickToUnlock4 / 1000f).ToString("0.0") + "k";
        }
        else
        {
            priceTextFormatted = minimumClickToUnlock4.ToString("0");
        }

        priceText.text = "Need " + priceTextFormatted + " score";
        amountText.text = "+" + (autoClicksPerSec4 + 5).ToString() + "/s";
    }
}
