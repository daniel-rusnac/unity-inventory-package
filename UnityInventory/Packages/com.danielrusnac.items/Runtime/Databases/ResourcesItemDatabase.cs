using System.Linq;
using UnityEngine;

namespace Items.Databases
{
    public class ResourcesItemDatabase : ItemDatabase
    {
        public ResourcesItemDatabase(string path) : base(LoadItems(path)) { }

        private static IItem[] LoadItems(string path)
        {
            return Resources
                .LoadAll<ScriptableObject>(path)
                .OfType<IItem>()
                .ToArray();
        }
    }
}