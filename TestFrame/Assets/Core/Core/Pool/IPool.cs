namespace SumBorn.Core
{
    public interface IPool<T>
    {
        T Get();
        void Push(T o);
        void Clear(bool invokePushAction = true);
    }
}