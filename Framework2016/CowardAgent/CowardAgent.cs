using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;

namespace CowardAgent
{
    public class CowardAgent : Agent
    {
        Random rnd = new Random();
        AIVector direction = new AIVector(0, 0);
        int lastDirectionAction = 0;
        int action = 0;
        int directionChangeDelay = 200;
        float bufferDistance = 5.0f;

        /// <summary>
        /// Constructer
        /// </summary>
        /// <param name="propertyStorage"></param>
        public CowardAgent(IPropertyStorage propertyStorage)
            : base(propertyStorage)
        {
            #region Stats
            //Max 250
            MovementSpeed = 110;//flat * movement
            Strength = 0;//flat dmg
            Health = 50;//flat health
            Eyesight = 60;//flat distance
            Endurance = 30;//hunger >= Endurance (30 -> 47 about sec)
            Dodge = 0;//flat chace of dodge
            if (MovementSpeed + Strength + Health + Eyesight + Endurance + Dodge > 250)
            {
                throw new Exception("Too high stats");
            }
            #endregion

        }

        /// <summary>
        /// Result of action
        /// </summary>
        /// <param name="success"></param>
        public override void ActionResultCallback(bool success)
        {
            //Do nothing - AI dont take success of an action into account - Yet
        }

        /// <summary>
        /// What to do next (Update) gets all objects in view distance
        /// </summary>
        /// <param name="otherEntities"></param>
        /// <returns></returns>
        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            action++;

            List<IEntity> nearEnemies = otherEntities.FindAll(x => x.GetType() != typeof(CowardAgent)
            && x is Agent && AIVector.Distance(Position, x.Position) < AIModifiers.maxMeleeAttackRange + bufferDistance);

            List<IEntity> nearCoward = otherEntities.FindAll(x => x.GetType() == typeof(CowardAgent)
            && x != this && AIVector.Distance(Position, x.Position) < AIModifiers.maxProcreateRange);

            List<IEntity> feedPlants = otherEntities.FindAll(x => x.GetType() == typeof(Plant)
            && AIVector.Distance(Position, x.Position) < AIModifiers.maxFeedingRange);

            List<IEntity> nearPlants = otherEntities.FindAll(x => x.GetType() == typeof(Plant) && x is Plant);

            if (nearCoward.Count > 0 && ProcreationCountDown <= 0)
            {
                foreach (CowardAgent coward in nearCoward)
                {
                    if (coward.ProcreationCountDown <= 0)
                    {
                        return new Procreate((CowardAgent)nearCoward[0]);//multiply
                    }
                }
            }
            if (nearEnemies.Count > 0)
            {
                foreach (Agent enemy in nearEnemies)
                {
                    if (!(enemy.Strength <= 0))
                    {
                        direction = new AIVector((enemy.Position.X - Position.X), (enemy.Position.Y - Position.Y)).Normalize() * -1;
                        return new Move(direction);//Flee
                    }
                }
            }
            if (feedPlants.Count > 0)
            {
                return new Feed((Plant)feedPlants[rnd.Next(feedPlants.Count)]);//eat
            }
            if (nearPlants.Count > 0)
            {
                direction = new AIVector((nearPlants[0].Position.X - Position.X), (nearPlants[0].Position.Y - Position.Y)).Normalize();
                return new Move(direction);//find food
            }
            if (lastDirectionAction + directionChangeDelay <= action)
            {
                lastDirectionAction = action;
                direction = new AIVector(rnd.Next(-1, 2), rnd.Next(-1, 2));
                return new Move(direction);//search direction
            }
            return new Move(direction);
        }
    }
}
