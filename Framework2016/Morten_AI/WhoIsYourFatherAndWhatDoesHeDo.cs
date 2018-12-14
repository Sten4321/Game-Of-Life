using System;
using System.Collections.Generic;
using System.Drawing;
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

        TimeSpan reProduceTimer;
        TimeSpan wallTimer;
        AIVector iMove;
        AIVector lastMovement;
        Random rnd = new Random();
        float lastFrameX;
        float lastFrameY;
        bool caughtOnWall = false;
        
        byte aiMovement;
        Agent enemy;

        public WhoIsYourFatherAndWhatDoesHeDo(IPropertyStorage propertyStorage)
            : base(propertyStorage)
        {
            
                
            MovementSpeed = 80;
            Strength = 50;
            Health = 10;
            Eyesight = 50;
            Endurance = 59;
            Dodge = 1;
                    
          
            
            

            string ddd = this.GetType().FullName;
        }


        bool loaded = true;
        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            if (loaded == true)
            {
                lastFrameX = Position.X;
                lastFrameY = Position.Y;
                loaded = false; 
            }
            List<Agent> agents = otherEntities.FindAll(a => a is Agent).ConvertAll<Agent>(a => (Agent)a);
            List<WhoIsYourFatherAndWhatDoesHeDo> ownAgents = otherEntities.FindAll(a => a is WhoIsYourFatherAndWhatDoesHeDo).ConvertAll<WhoIsYourFatherAndWhatDoesHeDo>(a => (WhoIsYourFatherAndWhatDoesHeDo)a);
            List<IEntity> plants = otherEntities.FindAll(a => a is Plant);
            iMove = null;

            

            #region My code
            //Clone
            if (iMove == null && reProduceTimer.Seconds <=0)
            {
                reProduceTimer = new TimeSpan(0, 1, 21);
                return new Procreate(this);
            }
            Console.WriteLine(Position.X);
            Console.WriteLine(Position.Y);
            //Attack
            if (iMove==null)
            {
                
                foreach (var a in agents)
                {
                    if (a.GetType() != typeof(WhoIsYourFatherAndWhatDoesHeDo))
                    {
                        enemy = a;
                        break;
                    }
                }
                if (enemy != null && enemy.Strength < 30)
                {
                    if (((enemy.Position.X - Position.X) < 10) && ((enemy.Position.Y - Position.Y) < 10))
                    {
                        return new Attack(enemy);
                    }
                    iMove = new AIVector((enemy.Position.X - Position.X), (enemy.Position.Y - Position.Y));
                }
            }

            //Eat
            if (iMove == null)
            {
                if (plants.Count > 0)
                {
                    var plant = plants.ElementAt(0);
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
            //CaughtOnWall
            if (caughtOnWall == true && wallTimer.Seconds >= 0)
            {
                if (lastFrameX > 990)
                {
                    iMove = new AIVector(-1, 0);
                }
                else if (lastFrameX < 1)
                {
                    iMove = new AIVector(1, 0);
                }
                else if (lastFrameY > 560)
                {
                    iMove = new AIVector(0, -1);
                }
                else if (lastFrameY < 1)
                {
                    iMove = new AIVector(0, 1);
                }
            }

            //Movement
            if (iMove == null)
            {
                Movement();
            }
            
            lastFrameX = this.Position.X;
            lastFrameY = this.Position.Y;
            return new Move(iMove);
            #endregion
        }

        public override void ActionResultCallback(bool success)
        {
            //Do nothing - AI dont take success of an action into account
           
        }
        
        public void Movement()
        {
            rnd = new Random();
            aiMovement = (byte)rnd.Next(1, 500);
            if (lastFrameX == Position.X || lastFrameY == Position.Y)
            {
                caughtOnWall = true;
                wallTimer = new TimeSpan(0, 0, 3);
            }
            if (lastMovement == null)
            {
                aiMovement = 1;
            }
            
            switch (aiMovement)
            {
                case 1:
                    iMove = new AIVector(rnd.Next(-1,2), rnd.Next(-1, 2));
                    break;
                case 2:
                    iMove = new AIVector(rnd.Next(-1, 2), rnd.Next(-1, 2));
                    break;
                case 3:
                    iMove = new AIVector(rnd.Next(-1, 2), rnd.Next(-1, 2));
                    break;
                case 4:
                    iMove = new AIVector(rnd.Next(-1, 2), rnd.Next(-1, 2));
                    break;

                default:
                    iMove = lastMovement;
                    break;
            }

            lastMovement = iMove;
        }
    }
}
