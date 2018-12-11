﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;

namespace Morten_AI
{
    public class WhoIsYourFatherAndWhatDoesHeDo : Agent
    {
        List<Agent> agents = new List<Agent>();
        AIVector iMove;
        Random rnd;
        public WhoIsYourFatherAndWhatDoesHeDo(IPropertyStorage propertyStorage)
            : base(propertyStorage)
        {
            rnd = new Random();
            MovementSpeed = 97;
            Strength = 1;
            Health = 1;
            Eyesight = 130;
            Endurance = 20;
            Dodge = 1;
            

            string ddd = this.GetType().FullName;
        }



        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            List<Agent> agents = otherEntities.FindAll(a => a is Agent).ConvertAll<Agent>(a => (Agent)a);
            List<IEntity> plants = otherEntities.FindAll(a => a is Plant);
            iMove = null;
            //Clone
            if (iMove == null)
            {
                //return new Procreate(this);
            }

            //Eat
            if (iMove == null)
            {
                if (plants.Count > 0)
                {
                    var plant = plants.ElementAt(rnd.Next(plants.Count));
                    if (plant.Position.X - Position.X > 1 || plant.Position.Y - Position.Y > 1)
                    {
                        iMove = new AIVector((plant.Position.X - Position.X + 1), (plant.Position.Y - Position.Y + 1));
                    }
                    else if (plant.Position.X - Position.X < 1 || plant.Position.Y - Position.Y < 1)
                    {
                        return new Feed((Plant)plants[rnd.Next(plants.Count)]);
                    }

                }
            }
            //Attack
            if (iMove == null)
            {
                foreach (var agent in agents)
                {
                    if (agent.GetType() != typeof(WhoIsYourFatherAndWhatDoesHeDo))
                    {
                        if (agent.Position.X - Position.X > 1 && agent.Position.Y - Position.Y > 1)
                        {
                            iMove = new AIVector((agent.Position.X - Position.X + 1), (agent.Position.Y - Position.Y + 1));
                            break;
                        }
                        else if (agent.Position.X - Position.X < 1 && agent.Position.Y - Position.Y < 1)
                        {
                            return new Attack(agent);
                        }
                    }
                }
            }

            if (iMove == null)
            {

                //if (Position.X > 970)
                //{
                //    iMove = new AIVector(-1, 0);
                //}
                //else if (Position.X < 20)
                //{
                //    iMove = new AIVector(1, 0);
                //}
                //else if (Position.Y < 20)
                //{
                //    iMove = new AIVector(0, 1);
                //}
                //else if (Position.Y > 530)
                //{
                //    iMove = new AIVector(0, -1);
                //}
                if (iMove == null)
                {
                    iMove = new AIVector(rnd.Next(-1, 2), rnd.Next(-1, 2));
                }
            }
            Console.WriteLine(iMove.X.ToString());
            return new Move(iMove);
        }
        
        public override void ActionResultCallback(bool success)
        {
            //Do nothing - AI dont take success of an action into account
            
        }
    }
}
