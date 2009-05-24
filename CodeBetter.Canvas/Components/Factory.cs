namespace CodeBetter.Canvas
{
    using System;
    using Components;
    using Ninject;
    using Ninject.Modules;

    public static class Factory
    {
        private static readonly IKernel _kernel = new StandardKernel(new CoreDependencies());

        public static IKernel Kernel
        {
            get { return _kernel; }
        }

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }
        public static T Get<T>(Type type)
        {
            return (T) _kernel.Get(type);
        }
        public static T TryGet<T>()
        {
            return _kernel.TryGet<T>();
        }
        public static T TryGet<T>(Type type)
        {
            return (T)_kernel.TryGet(type);
        }

        public static void Load(INinjectModule module)
        {            
            _kernel.Load(module);
        }
    }
}