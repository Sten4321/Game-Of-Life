using System;
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
        Agent EnemyAgent;
        IEntity nearByPlant;
        int caseNumber=0;
        List<Agent> agents = new List<Agent>();

        Random rnd;

        //Only for randomization of movement
        float moveX = 10;
        float moveY = 10;

        public WhoIsYourFatherAndWhatDoesHeDo(IPropertyStorage propertyStorage)
            : base(propertyStorage)
        {
            rnd = new Random();
            MovementSpeed = 150;
            Strength = 1;
            Health = 1;
            Eyesight = 50;
            Endurance = 47;
            Dodge = 1;


            moveX = rnd.Next(-1, 2);
            moveY = rnd.Next(-1, 2);
            string ddd = this.GetType().FullName;
        }



        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            List<Agent> agents = otherEntities.FindAll(a => a is Agent).ConvertAll<Agent>(a => (Agent)a);
            List<IEntity> plants = otherEntities.FindAll(a => a is Plant);
            EnemyAgent = null;
            
            foreach (var agent in agents)
            {
                if (agent.GetType() != typeof(WhoIsYourFatherAndWhatDoesHeDo))
                {
                    if (agent.Position == this.Position)
                    {
                        caseNumber = 2;
                        break;
                    }
                    else
                    {
                        caseNumber = 4;
                    }
                }
                Console.WriteLine(agent.Position.X);
                Console.WriteLine(agent.Position.Y);
            }
            foreach (var plant in plants)
            {
                
            }
            switch (caseNumber)
            {
                case 1: //Procreate
                    return new Procreate(this);

                case 2: //Attack Melee
                    if (EnemyAgent != null)
                    {
                        return new Attack(EnemyAgent);
                    }
                    break;
                case 3: //Feed
                    if (plants.Count > 0)
                    {
                        return new Feed((Plant)plants[rnd.Next(plants.Count)]);
                    }
                    break;
                case 4: //Move
                    if (this.Position.X > 990)
                    {
                        return new Move(new AIVector(-2, 0));
                    }
                    else if (this.Position.X < 5)
                    {
                        return new Move(new AIVector(2, 0));
                    }
                    else if (this.Position.Y < 5)
                    {
                        return new Move(new AIVector(0, 2));
                    }
                    else if (this.Position.Y > 550)
                    {
                        return new Move(new AIVector(0, -2));
                    }
                    else
                    {

                    }
                    break;
                default:
                    return new Defend();
            }
            if (this.Position.X > 990)
            {
                return new Move(new AIVector(-2, 0));
            }
            else if (this.Position.X < 5)
            {
                return new Move(new AIVector(2, 0));
            }
            else if (this.Position.Y < 5)
            {
                return new Move(new AIVector(0, 2));
            }
            else if (this.Position.Y > 550)
            {
                return new Move(new AIVector(0, -2));
            }
            else
            {
                return new Move(new AIVector(2, 0));
            }
        }
        
        public override void ActionResultCallback(bool success)
        {
            //Do nothing - AI dont take success of an action into account
            
        }
    }
}
