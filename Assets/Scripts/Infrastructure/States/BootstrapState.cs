using Constants;
using Infrastructure.Loaders;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Containers;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Inputs;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IDiContainer _diContainer;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUpdatable _updatable;
        private readonly EventListenerContainer _eventListenerContainer;
        private readonly Camera _camera;
        private readonly TransformableContainer _transformableContainer;

        public BootstrapState(IStateMachine stateMachine, IDiContainer diContainer, ISceneLoader sceneLoader,
            IUpdatable updatable, EventListenerContainer eventListenerContainer, Camera camera,
            TransformableContainer transformableContainer)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _updatable = updatable;
            _eventListenerContainer = eventListenerContainer;
            _camera = camera;
            _transformableContainer = transformableContainer;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneNames.Initial, OnSceneLoaded);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _diContainer.Register<IAssetProvider>(new AssetProvider());
            _diContainer.Register<IInputService>(new InputService(_updatable, new PlayerInput()));
            RegisterFactories();
        }

        private void RegisterFactories()
        {
            _diContainer.Register(new WeaponFactory(_diContainer.GetService<IAssetProvider>(),
                _eventListenerContainer, _updatable, _camera, _transformableContainer));
            
            _diContainer.Register(new EnemyFactory(_diContainer.GetService<IAssetProvider>(),
                _eventListenerContainer, _camera, _updatable, _transformableContainer));
            
            _diContainer.Register(new SpawnerFactory(_diContainer.GetService<IAssetProvider>(),
                _eventListenerContainer, _updatable, _diContainer.GetService<EnemyFactory>(), _camera, 
                _transformableContainer));
            
            _diContainer.Register(new ShipFactory(_diContainer.GetService<IAssetProvider>(),
                _eventListenerContainer, _diContainer.GetService<IInputService>(), _updatable, _camera, 
                _transformableContainer));
        }

        private void OnSceneLoaded()
        {
            _stateMachine.Enter<LoadLevelState>();
        }
    }
}
