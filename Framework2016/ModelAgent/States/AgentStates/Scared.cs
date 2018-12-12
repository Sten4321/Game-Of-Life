using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIFramework.Entities;
using AIFramework.Actions;
using AIFramework;

namespace ModelAgent.States.AgentStates
{
    class Scared : IState<ModelBasedAgent>
    {
        private static Scared _instance;

        public static Scared Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Scared();
                }
                return _instance;
            }
        }

        private Scared()
        {

        }

        public void Enter(ModelBasedAgent obj)
        {

        }

        public void Execute(ModelBasedAgent obj)
        {

            // No other agent with range < AIModifiers.maxMeleeAttackRange
            List<IEntity> inRange = obj.Model.FindAll(x => x is Agent && x.EntityType != typeof(ModelBasedAgent).Name && AIVector.Distance(obj.Position, x.Position) < AIModifiers.maxMeleeAttackRange);
            if (inRange.Count == 0)
            {
                obj.Fsm.ChangeState(Mature.Instance);
                return;
            }


            //If speed is higher than any agent with AIModifiers.maxMeleeAttackRange
            bool speedIsHigher = true;
            foreach (Agent x in inRange)
            {
                if (x.MovementSpeed > obj.MovementSpeed)
                {
                    speedIsHigher = false;
                    break;
                }
            }
            if (speedIsHigher)
            {
                obj.NextAction = new Move(( obj.Position- inRange[0].Position));
                return;
            }

            //Default
            obj.NextAction = new Defend();

        }

        public void Exit(ModelBasedAgent obj)
        {
                
        }
    }
}
