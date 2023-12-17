using System.Collections.Generic;
using Newtonsoft.Json;

public class GameRepository : IGameRepository
{
    private const string SAVE_NAME = "SaveData";
    private const string IV = "testIv";
    private const string KEY = "testKey";
        
    private Dictionary<string, string> _gameState = new();
    private readonly SaveDataSystem _gameDataSystem;

    public GameRepository()
    {
        _gameDataSystem = new SaveDataSystem(IV, KEY);
    }
    
    public T GetData<T>()
    {
        return JsonConvert.DeserializeObject<T>(_gameState[typeof(T).Name]);
    }
    public bool TryGetData<T>(out T value)
    {
        if (_gameState.TryGetValue(typeof(T).Name, out var serializedData))
        {
            value = JsonConvert.DeserializeObject<T>(serializedData);
            return true;
        }

        value = default;
        return false;
    }
    public void SetData<T>(T value)
    {
        _gameState[typeof(T).Name] = JsonConvert.SerializeObject(value);
    }

    public void LoadState()
    {
        if (_gameDataSystem.IsDataExists(SAVE_NAME))
            _gameState = _gameDataSystem.LoadData<Dictionary<string, string>>(SAVE_NAME);
    }
    public void SaveState()
    {
        _gameDataSystem.SaveData(_gameState, SAVE_NAME);
    }
    public void DeleteState()
    {
        _gameDataSystem.DeleteData(SAVE_NAME);
    }
}