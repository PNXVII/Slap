using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoClickUpgrade : MonoBehaviour
{  
    public int autoClicksPerSec;
    public int minimumClickToUnlock;
    public AudioSource upgradeSound;

    private float addClicks;
    private Manager manager;
    public TextMeshProUGUI priceText, amountText;

    private void Start()
    {
        manager = Manager.instance; ;

        if(PlayerPrefs.GetInt("autoClicksPerSec") != 0)
        {
            autoClicksPerSec = PlayerPrefs.GetInt("autoClicksPerSec");
        }
        if(PlayerPrefs.GetInt("minimumClickToUnlock") != 0)
        {
            minimumClickToUnlock = PlayerPrefs.GetInt("minimumClickToUnlock");
        }
        UpdateText();
    }

    public void BuyUpgrade()
    {

        if (manager.TotalClicks >= minimumClickToUnlock)
        {
            manager.TotalClicks -= minimumClickToUnlock;

            autoClicksPerSec++;
            minimumClickToUnlock *= 2;

            PlayerPrefs.SetInt("autoClicksPerSec", autoClicksPerSec);
            PlayerPrefs.SetInt("minimumClickToUnlock", minimumClickToUnlock);
            UpdateText();

            if (upgradeSound != null)
            {
                upgradeSound.Play();
            }
        }
    }

    private void Update()
    {
        if (autoClicksPerSec > 0)
        {

            addClicks += autoClicksPerSec * Time.deltaTime;
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
        priceText.text = "Need " + minimumClickToUnlock.ToString("0") + " score";
        amountText.text = "+" + (autoClicksPerSec + 1).ToString() + "/s";
    }
}
