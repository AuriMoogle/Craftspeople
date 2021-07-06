
namespace CraftsPeople.Data
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Author-", menuName = "ScriptableObjects/Author")]
    public class Author : ScriptableObject
    {
        public string AuthorName;
        public Sprite ID;
    }
}
