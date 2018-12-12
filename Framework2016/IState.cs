using System;

public class IState
{
    public interface IState<T>
    {
        void Enter(T obj);
        void Execute(T obj);
        void Exit(T obj);
    }
}
