using UnityEngine;

namespace TZRpg.Character
{
    [System.Serializable]
    public class CharacterData
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public GameObject Prefab { get; private set; }

        public CharacterData(string name, int id, GameObject prefab)
        {
            Name = name;
            Id = id;
            Prefab = prefab;
        }
    }
}
