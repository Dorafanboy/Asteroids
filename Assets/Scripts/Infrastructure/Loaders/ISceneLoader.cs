using System;

namespace Infrastructure.Loaders
{
    public interface ISceneLoader
    {
        void Load(string name, Action onLoaded = null);
    }
}