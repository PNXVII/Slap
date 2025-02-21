using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoClickVer2 : MonoBehaviour
{
    public int clickPower; // �ӹǹ��ԡ���������鹵�͡�á�˹�觤���
    public int upgradeCost; // �Ҥ�����Ѻ�ѻ�ô
    public TextMeshProUGUI priceText, amountText; // UI �ʴ��Ҥ���Шӹǹ��ԡ�������
    public Button clickButton, upgradeButton; // ��������Ѻ������ѻ�ô
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

            // �ѹ�֡���
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
