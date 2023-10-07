//using GrandLine.Core.Enums;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace GrandLine
//{
//    public class SaveState : MonoBehaviour
//    {
//        // Start is called before the first frame update
//        void Start()
//        {
        
//        }

//        // Update is called once per frame
//        void Update()
//        {
        
//        }
//    }
//}


using GrandLine.Core.Enums;
using System;

namespace GrandLine.Systems.Savegame
{
    [Serializable]
    public class SaveState
    {
        public string Id;

        public SaveableTypes Type;

        public NavigationData State;
    }
}