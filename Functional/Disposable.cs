namespace System
{
    public static class Disposable
    {
        public static TResult Using<TDisposable, TResult>(Func<TDisposable> factory, Func<TDisposable, TResult> fn)
            where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return fn(disposable);
            }
        }

        public static TResult Using<TIn, TDisposable, TResult>(this TIn input, Func<TDisposable> factory, Func<TDisposable, TIn, TResult> fn)
            where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return fn(disposable, input);
            }
        }
    }
}