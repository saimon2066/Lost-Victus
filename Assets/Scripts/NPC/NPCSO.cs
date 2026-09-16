using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    [CreateAssetMenu(fileName = "NPC", menuName = "MENUNAME", order = 0)]
    public class NPCSO : ScriptableObject
    {
        public string DisplayName;
        public string[] Dialogues;
    }
}