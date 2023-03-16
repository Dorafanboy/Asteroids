using System;
using Entities.Guns;

namespace Entities.Pool
{
    public static class FuncExtention
    {
        public static T Create<T>(this Func<object, T> func, object obj) where T : ITransformable
        {
            return func(obj);
        }
    }
}