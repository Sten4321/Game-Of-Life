using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;
using CowardAgent.States.AgentStates;

namespace CowardAgent
{
    class Explore : IState<CowardAgent>
    {
        private static Explore _instance;
        Random rnd;

        public static Explore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Explore();
                }
                return _instance;
            }
        }

        private Explore()
        {
            rnd = new Random();
        }

        public void Enter(CowardAgent obj)
        {

        }

        public void Execute(CowardAgent obj)
        {
            //Change your Direction
            if ((obj.lastDirectionAction + obj.directionChangeDelay <= obj.action) || (obj.direction.X == 0 && obj.direction.Y == 0))
            {
                obj.lastDirectionAction = obj.action;
                obj.direction = new AIVector(rnd.Next(-1, 2), rnd.Next(-1, 2));
                obj.NextAction = new Move(obj.direction);//search direction
            }

            //Just Move already
            obj.NextAction = new Move(obj.direction);//move
            obj.FSM.ChangeState(Multiply.Instance);
        }

        public void Exit(CowardAgent obj)
        {

        }
    }
}
