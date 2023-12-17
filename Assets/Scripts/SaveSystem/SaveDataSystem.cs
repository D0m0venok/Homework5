using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public sealed class SaveDataSystem
{
    private readonly AES _aes;
    private readonly bool _withEncryption;

    public SaveDataSystem()
    {
        _withEncryption = false;
    }
    public SaveDataSystem(string iv, string key)
    {
        _aes = new AES(iv, key);
        _withEncryption = true;
    }
    
    public void SaveData<T>(T data, string filename)
    {
        try
        {
            using var stream = new FileStream(GetSavePath(filename), FileMode.Create);
            using var writer = new StreamWriter(stream);
            var stringData = JsonConvert.SerializeObject(data);
            
            if(_withEncryption)
                stringData = _aes.Encrypt(stringData);
            
            writer.Write(stringData);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw;
        }
    }
    public T LoadData<T>(string filename) where T : class
    {
        if (!IsDataExists(filename))
            return null;

        try
        {
            using var stream = new FileStream(GetSavePath(filename), FileMode.Open);
            using var reader = new StreamReader(stream);
            var stringData = reader.ReadToEnd();

            if (_withEncryption)
                stringData = _aes.Decrypt(stringData);
            
            return JsonConvert.DeserializeObject<T>(stringData);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return null;
        }
    }
    public bool DeleteData(string filename)
    {
        if (!IsDataExists(filename))
            return false;

        try
        {
            File.Delete(GetSavePath(filename));
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            return false;
        }

        return true;
    }
    public bool IsDataExists(string filename)
    {
        return File.Exists(GetSavePath(filename));
    }

    private string GetSavePath(string name)
    {
        return Path.Combine(Application.persistentDataPath, name);
    }
}