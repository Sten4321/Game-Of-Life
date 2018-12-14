using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;

namespace Speedy
{
    class FindFood : IState<Speedy>
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

        public void Enter(Speedy obj)
        {

        }

        public void Execute(Speedy obj)
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

        public void Exit(Speedy obj)
        {

        }
    }
}
