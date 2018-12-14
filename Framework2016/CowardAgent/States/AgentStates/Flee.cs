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
    class Flee : IState<Speedy>
    {
        private static Flee _instance;
        Random rnd;

        public static Flee Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Flee();
                }
                return _instance;
            }
        }

        private Flee()
        {
            rnd = new Random();
        }

        public void Enter(Speedy obj)
        {

        }

        public void Execute(Speedy obj)
        {
            List<IEntity> nearEnemies = obj.ViewedEntities.FindAll(x => x.GetType() != typeof(Speedy)
            && x is Agent && AIVector.Distance(obj.Position, x.Position) < AIModifiers.maxMeleeAttackRange + obj.enemyBufferDistance);

            //Flee you fools
            if (nearEnemies.Count > 0)
            {
                foreach (Agent enemy in nearEnemies)
                {
                    if (!(enemy.Strength <= 5))
                    {
                        int xy = rnd.Next(2);
                        obj.direction = new AIVector((enemy.Position.X - obj.Position.X), (enemy.Position.Y - obj.Position.Y)).Normalize() * -1;
                        if ((obj.lastDirectionAction + obj.directionChangeDelay <= obj.action))
                        {
                            if (xy == 0)//X Randomizer
                            {
                                obj.direction = obj.direction + new AIVector(0, rnd.Next(-1, 1));//flee a bit up or down
                            }
                            else//Y Randomizer
                            {
                                obj.direction = obj.direction + new AIVector(rnd.Next(-1, 1), 0);//fle a bit left or right
                            }
                            obj.lastDirectionAction = obj.action;
                        }
                        obj.NextAction = new Move(obj.direction);//Flee
                        break;
                    }
                }
            }
            obj.FSM.ChangeState(Eat.Instance);

        }

        public void Exit(Speedy obj)
        {

        }
    }
}
