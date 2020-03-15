using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

namespace NaviEnt.Data
{

    public class DataSaver : MonoBehaviour
    {
        public static DataSaver Instance;

        ES3Settings _settings = null;

        //public JsonSaver _jsonSaver = null;
        [SerializeField]    
        bool isEncryptionOn = false;
        


        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;

            _settings = new ES3Settings(ES3.EncryptionType.AES, "mypassword");
        }


        public void Save<T>(T data)
        {
            
            string name = GetClassName<T>(data);

            if(isEncryptionOn)

                ES3.Save<T>(name, data, name + "_saveData.dat", _settings);
            else
                ES3.Save<T>(name, data, name + "_saveData.dat");
            
            Debug.Log("DataSave Complete. Path: " + Application.persistentDataPath + "/" + name + "_saveData.dat" + " || isEncrypted : " + isEncryptionOn);

        }

        public T Load<T>(T data)
        {
            
            T loadedData;
            string name = GetClassName<T>(data);
            loadedData = ES3.Load<T>(name, name + "_saveData.dat");
            
            Debug.Log("DataLoad Complete. Path: " + Application.persistentDataPath + "/" + name + "_saveData.dat");
            return loadedData;
        }

        public bool FileExists<T>(T data)
        {
            string name = GetClassName<T>(data);
            return ES3.FileExists(name + "_saveData.dat");
        }

        public void SaveStruct<T>(T data)
        {
            string name = GetClassName(data);
            ES3.Save<T>(name,data, name + "_saveData.dat");
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