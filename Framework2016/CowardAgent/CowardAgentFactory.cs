using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIFramework;
using AIFramework.Entities;
using System.Diagnostics;
using System.Reflection;

namespace CowardAgent
{
    class CowardAgentFactory : AgentFactory
    {
        /// <summary>
        /// create base agent
        /// </summary>
        /// <param name="propertyStorage"></param>
        /// <returns></returns>
        public override Agent CreateAgent(IPropertyStorage propertyStorage)
        {
            return new CowardAgent(propertyStorage);
        }

        /// <summary>
        /// procreation change stats?
        /// </summary>
        /// <param name="parent1"></param>
        /// <param name="parent2"></param>
        /// <param name="propertyStorage"></param>
        /// <returns></returns>
        public override Agent CreateAgent(Agent parent1, Agent parent2, IPropertyStorage propertyStorage)
        {
            return new CowardAgent(propertyStorage);
        }

        public override Type ProvidedAgentType
        {
            get { return typeof(CowardAgent); }
        }

        public override string Creators
        {
            get { return "Stefan : Sten4321"; }
        }

        public override void RegisterWinners(List<Agent> sortedAfterDeathTime)
        {
            //Do data collection - Perhaps used to evolutionary algoritmen
        }
    }
}
