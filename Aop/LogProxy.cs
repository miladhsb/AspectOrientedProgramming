using System.Reflection;

namespace Aop
{
    public class LogProxy<T> : DispatchProxy
    {
       private T Target { get; set; }
        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            Console.WriteLine("before log");
            var result=   targetMethod.Invoke(Target, args);
            Console.WriteLine("after log");
            return result;
        }

        public static T SetProxy<T>(T taget) where T : class
        {
            var proxy = Create<T, LogProxy<T>>() as LogProxy<T>;
            proxy.Target = taget;
            return proxy as T;
        }

       

    }
}
