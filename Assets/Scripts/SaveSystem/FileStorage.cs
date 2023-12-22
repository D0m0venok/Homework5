using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    public sealed class FileStorage
    {
        private const string SAVE_NAME = "SaveData";

        private const string IV = "testIv";
        private const string KEY = "testKey";

        private readonly AES _aes;
        private readonly bool _withEncryption;

        public FileStorage(bool withEncryption)
        {
            _withEncryption = withEncryption;

            if (_withEncryption)
                _aes = new AES(IV, KEY);
        }

        public void SaveData(Dictionary<string, string> data)
        {
            try
            {
                using var stream = new FileStream(GetSavePath(), FileMode.Create);
                using var writer = new StreamWriter(stream);
                var stringData = JsonConvert.SerializeObject(data);

                if (_withEncryption)
                    stringData = _aes.Encrypt(stringData);

                writer.Write(stringData);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }
        public Dictionary<string, string> LoadData()
        {
            if (!IsDataExists())
                return null;

            try
            {
                using var stream = new FileStream(GetSavePath(), FileMode.Open);
                using var reader = new StreamReader(stream);
                var stringData = reader.ReadToEnd();

                if (_withEncryption)
                    stringData = _aes.Decrypt(stringData);

                return JsonConvert.DeserializeObject<Dictionary<string, string>>(stringData);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }
        public bool DeleteData()
        {
            if (!IsDataExists())
                return false;

            try
            {
                File.Delete(GetSavePath());
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return false;
            }

            return true;
        }
        public bool IsDataExists()
        {
            return File.Exists(GetSavePath());
        }

        private string GetSavePath()
        {
            return Path.Combine(Application.persistentDataPath, SAVE_NAME);
        }

    }
}