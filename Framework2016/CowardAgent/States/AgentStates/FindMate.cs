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
    class FindMate : IState<CowardAgent>
    {
        private static FindMate _instance;

        public static FindMate Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FindMate();
                }
                return _instance;
            }
        }

        private FindMate()
        {

        }

        public void Enter(CowardAgent obj)
        {

        }

        public void Execute(CowardAgent obj)
        {
            List<IEntity> nearCoward = obj.ViewedEntities.FindAll(x => x is CowardAgent
            && x != obj);

            //Find Your Mate
            if (nearCoward.Count > 0 && obj.ProcreationCountDown <= 0)
            {
                foreach (CowardAgent coward in nearCoward)
                {
                    if (coward.ProcreationCountDown <= 0)
                    {
                        obj.direction = new AIVector((coward.Position.X - obj.Position.X), (coward.Position.Y - obj.Position.Y)).Normalize();
                        obj.NextAction = new Move(obj.direction);//find mate
                        break;
                    }
                }
            }
            obj.FSM.ChangeState(FindFood.Instance);
        }

        public void Exit(CowardAgent obj)
        {

        }
    }
}
