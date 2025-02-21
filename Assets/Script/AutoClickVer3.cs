using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoClickVer3 : MonoBehaviour
{
    public int autoClicksPerSec3;
    public int minimumClickToUnlock3;
    public AudioSource upgradeSound;

    private float addClicks;
    private Manager manager;
    public TextMeshProUGUI priceText, amountText;

    private void Start()
    {
        manager = Manager.instance; ;

        if (PlayerPrefs.GetInt("autoClicksPerSec3") != 0)
        {
            autoClicksPerSec3 = PlayerPrefs.GetInt("autoClicksPerSec3");
        }
        if (PlayerPrefs.GetInt("minimumClickToUnlock3") != 0)
        {
            minimumClickToUnlock3 = PlayerPrefs.GetInt("minimumClickToUnlock3");
        }
        UpdateText();
    }

    public void BuyUpgrade()
    {

        if (manager.TotalClicks >= minimumClickToUnlock3)
        {
            manager.TotalClicks -= minimumClickToUnlock3;

            autoClicksPerSec3+=3;
            minimumClickToUnlock3 *= 2;

            PlayerPrefs.SetInt("autoClicksPerSec3", autoClicksPerSec3);
            PlayerPrefs.SetInt("minimumClickToUnlock3", minimumClickToUnlock3);
            UpdateText();

            if (upgradeSound != null)
            {
                upgradeSound.Play();
            }
        }
    }

    private void Update()
    {
        if (autoClicksPerSec3 > 0)
        {
            addClicks += autoClicksPerSec3 * Time.deltaTime;
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
        priceText.text = "Need " + minimumClickToUnlock3.ToString("0") + " score";
        amountText.text = "+" + (autoClicksPerSec3 + 3).ToString() + "/s";
    }
}
