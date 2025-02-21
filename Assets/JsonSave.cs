using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string PlayerName;
    public int PlayerLevel;
    public int PlayerHP;
    public int PlayerMana;
    public int PlayerMoney;
    public bool isOpenPvp;
}
public class JsonSave : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        var player = new PlayerData();
        player.PlayerName = "Gay";
        player.PlayerLevel = 99;
        player.PlayerHP = 1000000;
        player.PlayerMana = 100;
        player.PlayerMoney = 1;
        player.isOpenPvp = false;

        SavePlayerData(player);
    }

    private void SavePlayerData(PlayerData data)
    {
        var playerDataString = JsonUtility.ToJson(data);
        Debug.Log(playerDataString);

        //Press Ctrl + .
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", playerDataString);
        Debug.Log(Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string path = Application.persistentDataPath + "/savefile.json";
            if(File.Exists(path))
            {
                string playerDataJson = File.ReadAllText(path);
                PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);

                Debug.Log(playerData.PlayerName);
                Debug.Log(playerData.PlayerLevel);
                Debug.Log(playerData.PlayerHP);
                Debug.Log(playerData.PlayerMana);
                Debug.Log(playerData.PlayerMoney);
                Debug.Log(playerData.isOpenPvp);
            }
        }
    }
}
