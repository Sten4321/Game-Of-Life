using AIFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAgent
{
    class FakePropertyProvider : IPropertyStorage
    {

 


        public Guid Id
        {
            get;set;
        }

        public AIVector Position
        {
            get;
            set;
        }

        public float Hunger
        {
            get;
            set;
        }

        public float Hitpoints
        {
            get;
            set;
        }


        public bool Defending
        {
            get;
            set;
        }

        public float ProcreationCountDown
        {
            get;
            set;
        }

        public int MovementSpeed
        {
            get;
            set;
        }

        public int Strength
        {
            get;
            set;
        }

        public int Health
        {
            get;
            set;
        }

        public int Eyesight
        {
            get;
            set;
        }

        public int Endurance
        {
            get;
            set;
        }


        public int Dodge
        {
            get;
            set;
        }
    }
}
