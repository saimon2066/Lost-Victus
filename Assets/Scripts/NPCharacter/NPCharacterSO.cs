using Items;
using UnityEngine;

namespace NPCharacter
{
    [CreateAssetMenu(fileName = "NPCharacterSO", menuName = "NPCharacter", order = 0)]
    public class NPCharacterSO : ScriptableObject
    {
        public string DisplayName;
        public NPCharacterSO[] RequiredNPCs;
        public ItemSO RequiredItem;
        
        public Dialogue.Dialogue LockedDialogue;
        public Dialogue.Dialogue StartDialogue;
        public Dialogue.Dialogue ItemDialogue;
        public Dialogue.Dialogue WrongItemDialogue;
        public Dialogue.Dialogue AfterDialogue;

        [TextArea] public string StartQuest;
        [TextArea] public string ItemQuest;

    }
}