using Constants;
using Infrastructure.Loaders;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Containers;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Inputs;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IDiContainer _diContainer;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUpdatable _updatable;
        private readonly EventListenerContainer _eventListenerContainer;

        public BootstrapState(IStateMachine stateMachine, IDiContainer diContainer, ISceneLoader sceneLoader,
            IUpdatable updatable, EventListenerContainer eventListenerContainer)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _updatable = updatable;
            _eventListenerContainer = eventListenerContainer;
            
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
            _diContainer.Register<IFactory>(new Factory(_diContainer.GetService<IAssetProvider>(), 
                _updatable, _diContainer.GetService<IInputService>(), _eventListenerContainer));
        }

        private void OnSceneLoaded()
        {
            _stateMachine.Enter<LoadLevelState>();
        }
    }
}
