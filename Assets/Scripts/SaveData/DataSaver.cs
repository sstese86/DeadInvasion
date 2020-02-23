using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

namespace NaviEnt.Data
{
    public class JsonSaver
    {


        public void Save<T>(SaveData<T> data) where T: SaveData<T>
        {
            data.hashValue = String.Empty;
            string json = JsonUtility.ToJson(data);
            data.hashValue = DataSaver.Instance.GetSHA256(json);
            string saveFilename = DataSaver.Instance.GetSaveFilename(DataSaver.Instance.GetClassName<SaveData<T>>(data));

            FileStream fileStream = new FileStream(saveFilename, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
            Debug.Log("Save Complete. Path: " + saveFilename);
        }


    }


    public class DataSaver : MonoBehaviour
    {
        static DataSaver _instance;
        public static DataSaver Instance => _instance;
        
        public JsonSaver _jsonSaver = null;

        static readonly string _filename = "_saveData.dat";


        private void Awake()
        {
            if (_instance != null) Destroy(gameObject);
            else _instance = this;

            _jsonSaver = new JsonSaver();
        }

        
        public string GetSaveFilename(string filename)
        {
            return Application.persistentDataPath + "/" + filename + _filename;
        }


        public string GetHexStringFromHash(byte[] hash)
        {
            string hexString = string.Empty;
            foreach(byte b in hash)
            {
                hexString += b.ToString("x2");
            }
            return hexString;
        }


        public void DeleteSaveFile(string filename)
        {
            File.Delete(GetSaveFilename(filename));
        }

        public string GetClassName<T>(T t)
        {
            string result = string.Empty;
            string[] fullname = t.ToString().Split('.');
            int index = fullname.Length - 1;
            result = fullname[index];

            return result;

        }


        public string GetSHA256(string text)
        {
            byte[] textToBytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed mySHA256 = new SHA256Managed();
            byte[] hashValue = mySHA256.ComputeHash(textToBytes);
            return GetHexStringFromHash(hashValue);
        }
    }
}