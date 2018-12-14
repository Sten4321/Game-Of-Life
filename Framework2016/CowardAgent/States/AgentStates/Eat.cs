using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;
using Speedy.States.AgentStates;

namespace Speedy
{
    class Eat : IState<Speedy>
    {
        private static Eat _instance;
        Random rnd;

        public static Eat Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Eat();
                }
                return _instance;
            }
        }

        private Eat()
        {
            rnd = new Random();
        }

        public void Enter(Speedy obj)
        {

        }

        public void Execute(Speedy obj)
        {
            List<IEntity> feedPlants = obj.ViewedEntities.FindAll(x => x is Plant
            && AIVector.Distance(obj.Position, x.Position) < AIModifiers.maxFeedingRange);

            //Eat if posible
            if (feedPlants.Count > 0)
            {
                obj.NextAction = new Feed((Plant)feedPlants[rnd.Next(feedPlants.Count)]);//eat
            }
            obj.FSM.ChangeState(FindMate.Instance);
        }

        public void Exit(Speedy obj)
        {

        }
    }
}
