using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "Item", order = 0)]
    public class ItemSO : ScriptableObject
    {
        public string DisplayName;
        public GameObject Prefab;
    }
}