using System.Reflection;

namespace Aop
{







    public class LoggingProxy<T> : DispatchProxy where T : class
    {
        private T _target;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            foreach (var item in targetMethod.GetParameters().Select(p => p.Name))
            {
                // Log the method call before invoking it
                Console.WriteLine($"Entering {targetMethod.Name} ..{item}...");
            }
         

            // Invoke the target method
            var result = targetMethod.Invoke(_target, args);

            // Log the method call after invoking it
            Console.WriteLine($"Exiting {targetMethod.Name}...");

            return result;
        }

        public static T Create<b>(b target)
        {
            // Create a new instance of the LoggingProxy<T> class
            var proxy = Create<T, LoggingProxy<T>>() as LoggingProxy<T>;

            // Set the target object that the proxy will intercept method calls for
            proxy._target = target as T;

            return proxy as T;
        }

        //public static T Create(T target)
        //{
        //    // Create a new instance of the LoggingProxy<T> class
        //    var proxy = Create<T, LoggingProxy<T>>() as LoggingProxy<T>;

        //    // Set the target object that the proxy will intercept method calls for
        //    proxy._target = target;

        //    return proxy as T;
        //}
    }




    //public class LogProxy<T> : DispatchProxy
    //{
    //    private T Target { get; set; }
    //    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    //    {
    //        Console.WriteLine("before log");
    //        var result = targetMethod.Invoke(Target, args);
    //        Console.WriteLine("after log");
    //        return result;
    //    }

    //    public static T SetProxy<T>(T taget) where T : class
    //    {
    //        var proxy = Create<T, LogProxy<T>>() as LogProxy<T>;
    //        proxy.Target = taget;
    //        return proxy as T;
    //    }



    //}
    public class AopAction<T> : DispatchProxy
    {
        #region Private Fields
        private Action<MethodInfo, object[], object> ActAfter;
        private Action<MethodInfo, object[]> ActBefore;
        private Action<MethodInfo, object[], Exception> ActException;
        private T Decorated;
        #endregion Private Fields

        #region Public Methods
        public static T Create(T decorated, Action<MethodInfo, object[]> actBefore = null, Action<MethodInfo, object[], object> actAfter = null, Action<MethodInfo, object[], Exception> actException = null)
        {
            object proxy = Create<T, AopAction<T>>();
            SetParameters();
            return (T)proxy;
            void SetParameters()
            {
                var me = ((AopAction<T>)proxy);
                me.Decorated = decorated == null ? throw new ArgumentNullException(nameof(decorated)) : decorated;
                me.ActBefore = actBefore;
                me.ActAfter = actAfter;
                me.ActException = actException;
            }
        }
        #endregion Public Methods

        #region Protected Methods
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            _ = targetMethod ?? throw new ArgumentException(nameof(targetMethod));

            try
            {
                ActBefore?.Invoke(targetMethod, args);
                var result = targetMethod.Invoke(Decorated, args);
                ActAfter?.Invoke(targetMethod, args, result);
                return result;
            }
            catch (Exception ex)
            {
                ActException?.Invoke(targetMethod, args, ex);
                throw ex.InnerException;
            }
        }
        #endregion Protected Methods
    }


}
