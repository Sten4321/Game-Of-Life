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
    class DMG : IState<Speedy>
    {
        private static DMG _instance;

        public static DMG Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DMG();
                }
                return _instance;
            }
        }

        private DMG()
        {

        }

        public void Enter(Speedy obj)
        {

        }

        public void Execute(Speedy obj)
        {
            List<IEntity> nearEnemies = obj.ViewedEntities.FindAll(x => x.GetType() != typeof(Speedy)
            && x is Agent && AIVector.Distance(obj.Position, x.Position) < AIModifiers.maxMeleeAttackRange);

            //kill
            if (nearEnemies.Count > 0)
            {
                foreach (Agent enemy in nearEnemies)
                {
                    obj.NextAction = new Attack(enemy);
                    break;
                }
            }
            obj.FSM.ChangeState(Eat.Instance);
        }

        public void Exit(Speedy obj)
        {

        }
    }
}
