using System;

namespace GrandLine.Encounters
{
    internal interface IEncounter
    {
        public Action OnCompleted { get; set; }
    }
}