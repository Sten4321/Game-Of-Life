using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelAgent.States
{
    public interface IState<T>
    {
        void Enter(T obj);
        void Execute(T obj);
        void Exit(T obj);
    }
}
