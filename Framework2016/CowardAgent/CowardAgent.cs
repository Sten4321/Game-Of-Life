using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;
using Speedy.States.AgentStates;

namespace Speedy
{
    public enum cowardType { coward, nonCoward }

    public class Speedy : Agent
    {
        int id;
        Random rnd = new Random();
        public AIVector direction { get; set; } = new AIVector(0, 0);
        public AIVector lastPos { get; set; }
        public AIVector lastDirection { get; set; }
        public int lastDirectionAction { get; set; } = 0;
        public int action { get; set; } = 0;
        public int MoveCheckDelay { get; } = 5;
        public int directionChangeDelay { get; } = 300;
        public float enemyBufferDistance { get; } = 15.0f;
        public float bufferDistance { get; } = 20.0f;
        public cowardType cowardAgentType { get; private set; }

        FSM<Speedy> fsm;

        public FSM<Speedy> FSM
        {
            get { return fsm; }
        }

        IAction nextAction;
        List<IEntity> viewedEntities;

        public List<IEntity> ViewedEntities
        {
            get { return viewedEntities; }
        }

        public IAction NextAction
        {
            get { return nextAction; }
            set { nextAction = value; }
        }

        /// <summary>
        /// Constructer
        /// </summary>
        /// <param name="propertyStorage"></param>
        public Speedy(IPropertyStorage propertyStorage, int id, cowardType type)
            : base(propertyStorage)
        {
            cowardAgentType = type;
            this.id = id;
            #region Stats
            switch (type)
            {
                case cowardType.coward:
                    //Max 250
                    MovementSpeed = 160;//flat * movement
                    Strength = 0;//flat dmg
                    Health = 10;//flat health
                    Eyesight = 80;//flat distance
                    Endurance = 0;//if hunger below endurance gain health regain
                    Dodge = 0;//flat chace of dodge
                    break;
                case cowardType.nonCoward:
                    //Max 250
                    MovementSpeed = 70;//flat * movement
                    Strength = 70;//flat dmg
                    Health = 70;//flat health
                    Eyesight = 40;//flat distance
                    Endurance = 0;//if hunger below endurance gain health regain
                    Dodge = 0;//flat chace of dodge
                    break;
            }
            if (MovementSpeed + Strength + Health + Eyesight + Endurance + Dodge > 250)
            {
                throw new Exception("Too high stats");
            }
            #endregion

            //Start state created
            fsm = new FSM<Speedy>(this);
            fsm.ChangeState(Explore.Instance);
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
            viewedEntities = otherEntities;
            fsm.ChangeState(Multiply.Instance);

            nextAction = null;
            while (nextAction == null)
            {
                fsm.Update();
            }
            return nextAction;
        }
    }
}
