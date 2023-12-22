using GameEngine;
using UnityEngine;
using Zenject;

public sealed class GameInstaller : MonoInstaller
{
    [SerializeField] private UnitManager _unitManager;
    [SerializeField] private ResourceService _resourceService;
    [SerializeField] private SaveLoadManager _saveLoadManager;
    [SerializeField] private Transform _unitsContainer;
    [SerializeField] private Unit[] _unitPrefabs;

    
    public override void InstallBindings()
    {
        Container.Bind<UnitManager>().FromInstance(_unitManager).AsSingle().WithArguments(_unitsContainer);
        Container.Bind<ResourceService>().FromInstance(_resourceService).AsSingle();
        Container.Bind<SaveLoadManager>().FromInstance(_saveLoadManager).AsSingle();
        Container.Bind<UnitsPrefabsCollection>().AsSingle().WithArguments(_unitPrefabs);
        Container.Bind<UnitSpawnerDecorator>().AsSingle();
        
        Container.BindInterfacesTo<ResourcesLoader>().AsCached();
        Container.BindInterfacesTo<UnitsLoader>().AsCached();
        
        _unitManager.SetupUnits(FindObjectsOfType<Unit>());
        _resourceService.SetResources(FindObjectsOfType<Resource>());
    }
}