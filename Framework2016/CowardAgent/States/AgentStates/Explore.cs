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
            //Change your Direction
            if ((obj.lastDirectionAction + obj.directionChangeDelay <= obj.action) || (obj.direction.X == 0 && obj.direction.Y == 0))
            {
                obj.lastDirectionAction = obj.action;
                obj.direction = new AIVector(rnd.Next(-1, 2), rnd.Next(-1, 2));
            }
        }

        public void Execute(CowardAgent obj)
        {
            if (obj.lastDirectionAction + (obj.directionChangeDelay / 5) <= obj.action)
            {
                if (obj.lastPos != null)
                {
                    if (obj.lastPos.X == obj.Position.X && ((obj.lastDirection.X < 0 && obj.direction.X < 0) || (obj.lastDirection.X > 0 && obj.direction.X > 0)))
                    {
                        obj.direction.X = obj.direction.X * -1;
                    }
                    if (obj.lastPos.Y == obj.Position.Y && ((obj.lastDirection.Y < 0 && obj.direction.Y < 0) || (obj.lastDirection.Y > 0 && obj.direction.Y > 0)))
                    {
                        obj.direction.Y = obj.direction.Y * -1;
                    }
                }

                obj.lastDirection = new AIVector(obj.direction.X, obj.direction.Y);
                obj.lastPos = new AIVector(obj.Position.X, obj.Position.Y);
            }

            //Just Move already
            obj.NextAction = new Move(obj.direction);//move
        }

        public void Exit(CowardAgent obj)
        {

        }
    }
}
