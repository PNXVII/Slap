using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerData binaryPlayerData = new PlayerData();
        binaryPlayerData.PlayerName = gameObject.name;
        binaryPlayerData.PlayerLevel = 100;
        binaryPlayerData.PlayerHP = 1;
        binaryPlayerData.PlayerMana = 1;
        binaryPlayerData.PlayerMoney = 1;
        binaryPlayerData.isOpenPvp = true;

        SaveBinary(binaryPlayerData);
    }

    void SaveBinary(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Create(Application.persistentDataPath + "/save.dat");
        formatter.Serialize(stream, data);
        stream.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            string path = Application.persistentDataPath + "/save.dat";
            if(File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = File.Open(path, FileMode.Open);
                PlayerData binaryPlayerData = (PlayerData)formatter.Deserialize(stream);
                stream.Close();

                Debug.Log(binaryPlayerData.PlayerName);
                Debug.Log(binaryPlayerData.PlayerLevel);
                Debug.Log(binaryPlayerData.PlayerHP);
                Debug.Log(binaryPlayerData.PlayerMana);
                Debug.Log(binaryPlayerData.PlayerMoney);
                Debug.Log(binaryPlayerData.isOpenPvp);
            }
        }
    }
}
