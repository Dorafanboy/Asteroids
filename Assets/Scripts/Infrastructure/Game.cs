using Infrastructure.Loaders;
using Infrastructure.Services.Containers;
using Infrastructure.States;

namespace Infrastructure
{
    public class Game
    {
        public Game(IUpdatable updatable, ICoroutineRunner coroutineRunner)
        {
            var sceneLoader = new SceneLoader(coroutineRunner);
            var diContainer = new DiContainer();
            var stateMachine = new StateMachine(sceneLoader, diContainer, updatable);

            stateMachine.Enter<BootstrapState>();
        }
    }
}