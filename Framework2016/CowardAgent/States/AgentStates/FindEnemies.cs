﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;

namespace Speedy
{
    class FindEnemies : IState<Speedy>
    {
        private static FindEnemies _instance;

        public static FindEnemies Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FindEnemies();
                }
                return _instance;
            }
        }

        private FindEnemies()
        {

        }

        public void Enter(Speedy obj)
        {

        }

        public void Execute(Speedy obj)
        {
            List<IEntity> nearEnemies = obj.ViewedEntities.FindAll(x => x.GetType() != typeof(Speedy)
            && x is Agent);

            //Find Your Mate
            if (nearEnemies.Count > 0)
            {
                foreach (Agent enemy in nearEnemies)
                {
                    obj.direction = new AIVector((enemy.Position.X - obj.Position.X), (enemy.Position.Y - obj.Position.Y)).Normalize();
                    obj.NextAction = new Move(obj.direction);//find mate
                    break;
                }
            }

            obj.FSM.ChangeState(FindFood.Instance);
        }

        public void Exit(Speedy obj)
        {

        }
    }
}
