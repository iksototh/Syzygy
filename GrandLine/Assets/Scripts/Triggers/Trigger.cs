
using UnityEngine;

namespace GrandLine.Triggers
{
    public class Trigger : ITrigger
    {
        public void OnTrigger()
        {
            Debug.Log("TRIGGER");
        }
    }
}