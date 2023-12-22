using System.Collections.Generic;
using Newtonsoft.Json;

namespace SaveSystem
{
    public sealed class GameRepository : IGameRepository
    {
        private Dictionary<string, string> _gameState = new();
        private readonly FileStorage _fileStorage = new(false);

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
            if (_fileStorage.IsDataExists())
                _gameState = _fileStorage.LoadData();
        }
        public void SaveState()
        {
            _fileStorage.SaveData(_gameState);
        }
        public void DeleteState()
        {
            _fileStorage.DeleteData();
        }
    }
}