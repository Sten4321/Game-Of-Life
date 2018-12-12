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
    class Eat : IState<CowardAgent>
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

        public void Enter(CowardAgent obj)
        {

        }

        public void Execute(CowardAgent obj)
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

        public void Exit(CowardAgent obj)
        {

        }
    }
}
