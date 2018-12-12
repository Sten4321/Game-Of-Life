using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;
using CowardAgent.States.AgentStates;

namespace CowardAgent
{
    public class CowardAgent : Agent
    {
        int id;
        Random rnd = new Random();
        public AIVector direction { get; set; } = new AIVector(0, 0);
        public int lastDirectionAction { get; set; } = 0;
        public int action { get; set; } = 0;
        public int MoveCheckDelay { get; } = 5;
        public int directionChangeDelay { get; } = 250;
        public float enemyBufferDistance { get; } = 15.0f;
        public float bufferDistance { get; } = 20.0f;

        FSM<CowardAgent> fsm;

        public FSM<CowardAgent> FSM
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
        public CowardAgent(IPropertyStorage propertyStorage, int id)
            : base(propertyStorage)
        {
            this.id = id;
            #region Stats
            //Max 250
            MovementSpeed = 160;//flat * movement
            Strength = 0;//flat dmg
            Health = 10;//flat health
            Eyesight = 80;//flat distance
            Endurance = 0;//if hunger below endurance gain health regain
            Dodge = 0;//flat chace of dodge
            if (MovementSpeed + Strength + Health + Eyesight + Endurance + Dodge > 250)
            {
                throw new Exception("Too high stats");
            }
            #endregion

            //Start state created
            fsm = new FSM<CowardAgent>(this);
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
