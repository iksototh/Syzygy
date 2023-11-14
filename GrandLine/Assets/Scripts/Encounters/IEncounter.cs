using System;

namespace GrandLine.Encounters
{
    public interface IEncounter
    {
        public void Accept(Action complete);
    }
}