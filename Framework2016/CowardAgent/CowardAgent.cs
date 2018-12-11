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
        int id;
        static Random rnd = new Random();
        AIVector direction = new AIVector(0, 0);
        int lastDirectionAction = 0;
        int action = 0;
        const int MoveCheckDelay = 5;
        const int directionChangeDelay = 250;
        const float bufferDistance = 20.0f;
        const int hight = 579;
        const int width = 1012;

        /// <summary>
        /// Constructer
        /// </summary>
        /// <param name="propertyStorage"></param>
        public CowardAgent(IPropertyStorage propertyStorage, int id)
            : base(propertyStorage)
        {
            this.id = id;
            #region Stats
            //Max 250
            MovementSpeed = 145;//flat * movement
            Strength = 0;//flat dmg
            Health = 20;//flat health
            Eyesight = 80;//flat distance
            Endurance = 5;//hunger >= Endurance (30 -> 47 about sec)
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

            List<IEntity> proCoward = otherEntities.FindAll(x => x is CowardAgent
            && x != this && AIVector.Distance(Position, x.Position) < AIModifiers.maxProcreateRange);

            List<IEntity> nearCoward = otherEntities.FindAll(x => x is CowardAgent
            && x != this);

            List<IEntity> feedPlants = otherEntities.FindAll(x => x is Plant
            && AIVector.Distance(Position, x.Position) < AIModifiers.maxFeedingRange);

            List<IEntity> nearPlants = otherEntities.FindAll(x => x is Plant);

            //Mate Now Multiply MORE
            if (proCoward.Count > 0 && ProcreationCountDown <= 0)
            {
                foreach (CowardAgent coward in proCoward)
                {
                    if (coward.ProcreationCountDown <= 0)
                    {
                        return new Procreate((CowardAgent)proCoward[0]);//multiply
                    }
                }
            }

            //Don't get stuck
            if (MoveAwayFromWalls() != null)
            {
                return MoveAwayFromWalls();
            }

            //Flee you fools
            if (nearEnemies.Count > 0)
            {
                foreach (Agent enemy in nearEnemies)
                {
                    if (!(enemy.Strength <= 5))
                    {
                        int xy = rnd.Next(2);
                        direction = new AIVector((enemy.Position.X - Position.X), (enemy.Position.Y - Position.Y)).Normalize() * -1;
                        if ((lastDirectionAction + directionChangeDelay <= action))
                        {
                            if (xy == 0)//X Randomizer
                            {
                                direction = direction + new AIVector(0, rnd.Next(-1, 1));//flee a bit up or down
                            }
                            else//Y Randomizer
                            {
                                direction = direction + new AIVector(rnd.Next(-1, 1), 0);//fle a bit left or right
                            }
                            lastDirectionAction = action;
                        }
                        return new Move(direction);//Flee
                    }
                }
            }

            //Eat if posible
            if (feedPlants.Count > 0)
            {
                return new Feed((Plant)feedPlants[rnd.Next(feedPlants.Count)]);//eat
            }

            //Find Your Mate
            if (nearCoward.Count > 0 && ProcreationCountDown <= 0)
            {
                foreach (CowardAgent coward in nearCoward)
                {
                    if (coward.ProcreationCountDown <= 0)
                    {
                        direction = new AIVector((coward.Position.X - Position.X), (coward.Position.Y - Position.Y)).Normalize();
                        return new Move(direction);//find mate
                    }
                }
            }

            //Find Food
            if (nearPlants.Count > 0)
            {
                direction = new AIVector((nearPlants[0].Position.X - Position.X), (nearPlants[0].Position.Y - Position.Y)).Normalize();
                return new Move(direction);//find food
            }

            //Change your Direction
            if ((lastDirectionAction + directionChangeDelay <= action) || (direction.X == 0 && direction.Y == 0))
            {
                lastDirectionAction = action;
                direction = new AIVector(rnd.Next(-1, 2), rnd.Next(-1, 2));
                return new Move(direction);//search direction
            }

            //Just Move already
            return new Move(direction);//move
        }

        /// <summary>
        /// moves away from wall
        /// </summary>
        private IAction MoveAwayFromWalls()
        {
            float x = 0;
            float y = 0;
            if (Position.X < bufferDistance)
            {
                x++;
            }
            else if (Position.X > width - bufferDistance)
            {
                x--;
            }
            if (Position.Y < bufferDistance)
            {
                y++;
            }
            else if (Position.Y > hight - bufferDistance)
            {
                y--;
            }
            if (x != 0 && y != 0)
            {
                lastDirectionAction = action;
                direction = new AIVector(x, y);
                return new Move(direction);//search direction
            }
            return null;
        }
    }
}
