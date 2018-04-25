namespace System
{
    public static class FunctionalExtensions
    {
        public static T Tee<T>(this T @this, Action<T> action)
        {
            action(@this);
            return @this;
        }

        public static TResult Map<TSource, TResult>(this TSource @this, Func<TSource, TResult> fn) =>
            fn(@this);

        public static T When<T>(this T @this, Func<bool> predicate, Func<T, T> fn) =>
            predicate()
                ? fn(@this)
                : @this;

        public static T When<T>(this T @this, Func<T, bool> predicate, Func<T, T> fn) =>
            predicate(@this)
                ? fn(@this)
                : @this;
    }
}