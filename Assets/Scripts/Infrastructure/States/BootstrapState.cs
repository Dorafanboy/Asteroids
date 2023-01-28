using Infrastructure.Loaders;
using Infrastructure.Services;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Inputs;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Main = "Main";
        
        private readonly IStateMachine _stateMachine;
        private readonly IDiContainer _diContainer;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUpdatable _updatable;

        public BootstrapState(IStateMachine stateMachine, IDiContainer diContainer, ISceneLoader sceneLoader, IUpdatable updatable)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _updatable = updatable;
            
            RegisterServices();
        }

        public void Enter()
        {
            _stateMachine.Enter<LoadLevelState>();
            //_sceneLoader.Load(Main);
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            _diContainer.Register<IAssetProvider>(new AssetProvider());
            _diContainer.Register<IFactory>(new Factory(_diContainer.GetService<IAssetProvider>(), _updatable, 
                _diContainer.GetService<IInputService>()));
            _diContainer.Register<IInputService>(new InputService(_updatable));
        }

        private void OnSceneLoaded()
        {
            //_stateMachine.Enter<LoadLevelState>();
        }
    }
}