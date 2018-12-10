using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;

namespace Morten_AI
{
    public class RandomAgentFactory : AgentFactory
    {
        public override Agent CreateAgent(IPropertyStorage propertyStorage)
        {
            return new WhoIsYourFatherAndWhatDoesHeDo(propertyStorage);
        }

        public override Agent CreateAgent(Agent parent1, Agent parent2, IPropertyStorage propertyStorage)
        {
            
            return new WhoIsYourFatherAndWhatDoesHeDo(propertyStorage);
        }

        public override Type ProvidedAgentType
        {
            get { return typeof(WhoIsYourFatherAndWhatDoesHeDo); }
        }

        public override string Creators
        {
            get { return "Morten!"; }
        }

        public override void RegisterWinners(List<Agent> sortedAfterDeathTime)
        {
            //Do data collection - Perhaps used to evolutionary algoritmen
        }
    }
}
