using Constants;
using Infrastructure.Loaders;

namespace Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public LoadLevelState(IStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(Constant.Main, OnSceneLoaded); 
        }

        public void Exit()
        {
        }
        
        private void OnSceneLoaded()
        {
            _stateMachine.Enter<InitialState>();
        }
    }
}