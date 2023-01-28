using Infrastructure.Loaders;

namespace Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private const string Main = "Main";
        
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public LoadLevelState(IStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            //_sceneLoader.Load(Main); 
            _stateMachine.Enter<InitialState>();
        }

        public void Exit()
        {
            
        }
    }
}