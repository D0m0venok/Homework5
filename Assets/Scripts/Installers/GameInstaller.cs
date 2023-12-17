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
        Container.Bind<UnitsPrefabsManager>().AsSingle().WithArguments(_unitPrefabs);
        Container.BindInterfacesTo<ResourcesLoader>().AsSingle();
        Container.BindInterfacesTo<UnitsLoader>().AsSingle();
        
        _unitManager.SetupUnits(FindObjectsOfType<Unit>());
        _resourceService.SetResources(FindObjectsOfType<Resource>());
    }
}