using Zenject;

namespace SaveSystem
{
    public interface ISaveLoader
    {
        void SaveGame(IGameRepository gameRepository, DiContainer container);
        void LoadGame(IGameRepository gameRepository, DiContainer container);
    }
}