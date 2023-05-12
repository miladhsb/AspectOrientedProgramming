using System.Reflection;

namespace Aop
{
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
    }
}
