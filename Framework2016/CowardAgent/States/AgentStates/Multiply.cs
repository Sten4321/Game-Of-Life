﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;

namespace Speedy.States.AgentStates
{
    class Multiply : IState<Speedy>
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

        public void Enter(Speedy obj)
        {

        }

        public void Execute(Speedy obj)
        {
            List<IEntity> proCoward = obj.ViewedEntities.FindAll(x => x is Speedy
            && x != obj && AIVector.Distance(obj.Position, x.Position) < AIModifiers.maxProcreateRange);

            //Mate Now Multiply MORE
            if (proCoward.Count > 0 && obj.ProcreationCountDown <= 0)
            {
                foreach (Speedy coward in proCoward)
                {
                    if (coward.ProcreationCountDown <= 0)
                    {
                        obj.NextAction = new Procreate(coward);//multiply
                        break;
                    }
                }
            }
            switch (obj.cowardAgentType)
            {
                case cowardType.coward:
                    obj.FSM.ChangeState(Flee.Instance);
                    break;
                case cowardType.nonCoward:
                    obj.FSM.ChangeState(DMG.Instance);
                    break;
                default:
                    obj.FSM.ChangeState(Flee.Instance);
                    break;
            }
        }

        public void Exit(Speedy obj)
        {

        }
    }
}
