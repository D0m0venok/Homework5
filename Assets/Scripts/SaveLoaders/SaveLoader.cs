using UnityEngine;
using Zenject;

public abstract class SaveLoader<TData, TService> : ISaveLoader
{
    public void SaveGame(IGameRepository gameRepository, DiContainer container)
    {
        var service = container.Resolve<TService>();
        var data = ConvertToData(service);
        gameRepository.SetData(data);
        
        Debug.Log($"<color=green>Data saved: {typeof(TData)}!</color>");
    }
    public void LoadGame(IGameRepository gameRepository, DiContainer container)
    {
        var service = container.Resolve<TService>();

        if (gameRepository.TryGetData(out TData data))
        {
            SetData(data, service);
            Debug.Log($"<color=green>Data loaded: {typeof(TData)}!</color>");
        }
    }

    protected abstract TData ConvertToData(TService service);
    protected abstract void SetData(TData data, TService service);
}