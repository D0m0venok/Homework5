using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    public sealed class SaveLoadManager : MonoBehaviour
    {
        private DiContainer _diContainer;
        private GameRepository _gameRepository;
        private ISaveLoader[] _saveLoaders;

        [Inject]
        public void Construct(DiContainer diContainer, ISaveLoader[] saveLoaders)
        {
            _diContainer = diContainer;
            _saveLoaders = saveLoaders;
            _gameRepository = new GameRepository();
        }
        [Button]
        public void LoadGame()
        {
            _gameRepository.LoadState();
        
            foreach (var loader in _saveLoaders)
            {
                loader.LoadGame(_gameRepository, _diContainer);
            }
        }
        [Button]
        public void SaveGame()
        {
            foreach (var loader in _saveLoaders)
            {
                loader.SaveGame(_gameRepository, _diContainer);
            }
        
            _gameRepository.SaveState();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if(pauseStatus)
                SaveGame();
        }
    }
}