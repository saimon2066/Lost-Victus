using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "ItemSO", order = 0)]
    public class ItemSO : ScriptableObject
    {
        public string DisplayName;
    }
}