using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;

namespace CowardAgent.States.AgentStates
{
    class Multiply : IState<CowardAgent>
    {
        private static Multiply _instance;

        public static Multiply Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Multiply();
                }
                return _instance;
            }
        }

        private Multiply()
        {

        }

        public void Enter(CowardAgent obj)
        {
            
        }

        public void Execute(CowardAgent obj)
        {
            List<IEntity> proCoward = obj.ViewedEntities.FindAll(x => x is CowardAgent
            && x != obj && AIVector.Distance(obj.Position, x.Position) < AIModifiers.maxProcreateRange);

            //Mate Now Multiply MORE
            if (proCoward.Count > 0 && obj.ProcreationCountDown <= 0)
            {
                foreach (CowardAgent coward in proCoward)
                {
                    if (coward.ProcreationCountDown <= 0)
                    {
                        obj.NextAction = new Procreate(coward);//multiply
                        break;
                    }
                }
            }
            obj.FSM.ChangeState(Flee.Instance);
        }

        public void Exit(CowardAgent obj)
        {

        }
    }
}
