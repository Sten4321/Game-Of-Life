using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;

namespace CowardAgent
{
    class FindFood : IState<CowardAgent>
    {
        private static FindFood _instance;

        public static FindFood Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FindFood();
                }
                return _instance;
            }
        }

        private FindFood()
        {

        }

        public void Enter(CowardAgent obj)
        {

        }

        public void Execute(CowardAgent obj)
        {
            List<IEntity> nearPlants = obj.ViewedEntities.FindAll(x => x is Plant);

            //Find Food
            if (nearPlants.Count > 0)
            {
                obj.direction = new AIVector((nearPlants[0].Position.X - obj.Position.X), (nearPlants[0].Position.Y - obj.Position.Y)).Normalize();
                obj.NextAction= new Move(obj.direction);//find food
            }
            obj.FSM.ChangeState(Explore.Instance);
        }

        public void Exit(CowardAgent obj)
        {

        }
    }
}
