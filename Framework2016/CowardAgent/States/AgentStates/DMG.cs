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
    class DMG : IState<CowardAgent>
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

        public void Enter(CowardAgent obj)
        {

        }

        public void Execute(CowardAgent obj)
        {
            List<IEntity> nearEnemies = obj.ViewedEntities.FindAll(x => x.GetType() != typeof(CowardAgent)
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

        public void Exit(CowardAgent obj)
        {

        }
    }
}
