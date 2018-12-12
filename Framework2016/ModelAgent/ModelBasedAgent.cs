using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;
using ModelAgent.States;
using ModelAgent.States.AgentStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAgent
{
    class ModelBasedAgent : Agent
    {

        List<IEntity> model = new List<IEntity>();

        internal List<IEntity> Model
        {
            get { return model; }
        }

        #region FSM
        FSM<ModelBasedAgent> fsm;

        public FSM<ModelBasedAgent> Fsm
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

        #endregion


        public ModelBasedAgent(IPropertyStorage propertyStorage)
            : base(propertyStorage)
        {
            MovementSpeed = 50;
            Strength = 65;
            Health = 10;
            Eyesight = 35;
            Endurance = 35;
            Dodge = 55;

            fsm = new FSM<ModelBasedAgent>(this);
            fsm.ChangeState(Born.Instance);

        }

        private void UpdateModel(List<IEntity> otherEntities)
        {
            foreach (IEntity e in otherEntities)
            {
                if (e is Agent)
                {
                    Agent a = (Agent)e;

                    //Is agent already in model
                    CachedAgent modelAgent = (CachedAgent) model.Find(f => f.Id.ToString() == a.Id.ToString());
                    if (modelAgent != null)
                    {
                        //Update agent information
                        modelAgent.UpdateInformation(a);
                    }
                    else
                    {
                        //Add new agent
                        model.Add(new CachedAgent(a, new FakePropertyProvider()));
                    }

                }
            }
        }

        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            //Update Model with lastest information about agents
            UpdateModel(otherEntities);

            //Create new empty list
            viewedEntities = otherEntities;


            nextAction = null;
            while (nextAction == null)
            {
                fsm.Update();
            }
            return nextAction;
        }

        public override void ActionResultCallback(bool success)
        {
            //throw new NotImplementedException();
        }


    }
}
