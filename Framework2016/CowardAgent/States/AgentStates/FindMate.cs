using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;

namespace Speedy.States.AgentStates
{
    class FindMate : IState<Speedy>
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

        public void Enter(Speedy obj)
        {

        }

        public void Execute(Speedy obj)
        {
            List<IEntity> nearCoward = obj.ViewedEntities.FindAll(x => x is Speedy
            && x != obj);

            //Find Your Mate
            if (nearCoward.Count > 0 && obj.ProcreationCountDown <= 0)
            {
                foreach (Speedy coward in nearCoward)
                {
                    if (coward.ProcreationCountDown <= 0)
                    {
                        obj.direction = new AIVector((coward.Position.X - obj.Position.X), (coward.Position.Y - obj.Position.Y)).Normalize();
                        obj.NextAction = new Move(obj.direction);//find mate
                        break;
                    }
                }
            }
            switch (obj.cowardAgentType)
            {
                case cowardType.coward:
                    obj.FSM.ChangeState(FindFood.Instance);
                    break;
                case cowardType.nonCoward:
                    if (!(obj.Hunger > 60))
                    {
                        obj.FSM.ChangeState(FindEnemies.Instance);
                    }
                    else
                    {
                        obj.FSM.ChangeState(FindFood.Instance);
                    }
                    break;
                default:
                    obj.FSM.ChangeState(FindFood.Instance);
                    break;
            }
        }

        public void Exit(Speedy obj)
        {

        }
    }
}
