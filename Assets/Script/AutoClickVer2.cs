using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoClickVer2 : MonoBehaviour
{
    public int clickPower; // จำนวนคลิกที่เพิ่มขึ้นต่อการกดหนึ่งครั้ง
    public int upgradeCost; // ราคาสำหรับอัปเกรด
    public TextMeshProUGUI priceText, amountText; // UI แสดงราคาและจำนวนคลิกที่เพิ่ม
    public Button clickButton, upgradeButton; // ปุ่มสำหรับกดและอัปเกรด
    private Manager manager;
    public AudioSource upgradeSound;

    private void Start()
    {
        manager = Manager.instance;

        clickPower = PlayerPrefs.GetInt("clickPower", clickPower);
        upgradeCost = PlayerPrefs.GetInt("upgradeCost", upgradeCost);

        UpdateText();

        clickButton.onClick.AddListener(AddClick);
        upgradeButton.onClick.AddListener(BuyUpgrade);
    }
    public void AddClick()
    {
        manager.AddClicks(clickPower);
    }

    public void BuyUpgrade()
    {
        if (manager.TotalClicks >= upgradeCost)
        {
            manager.TotalClicks -= upgradeCost; 
            clickPower += 1; 
            upgradeCost *= 2; 

            // บันทึกค่า
            PlayerPrefs.SetInt("clickPower", clickPower);
            PlayerPrefs.SetInt("upgradeCost", upgradeCost);

            UpdateText();

            if (upgradeSound != null)
            {
                upgradeSound.Play();
            }
        }
    }

    private void UpdateText()
    {
        priceText.text = "Upgrade need: " + upgradeCost.ToString("0");
        amountText.text = "Click Power: +" + clickPower.ToString();
    }
}
