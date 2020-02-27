using System;
using UnityEngine;

namespace Librarian
{
    public class Item
    {
        private string _PrefabPath;
        private SceneItem _Owner;
        private float[] _Bonuses;

        public Item(SceneItem owner, string prefabPath, float boredomBonus, float fearBonus, float tirednessBonus)
        {
            _Owner = owner;
            _PrefabPath = prefabPath;

            _Bonuses = new float[Enum.GetNames(typeof(Feeling)).Length];
            _Bonuses[(int)Feeling.Boredom] = boredomBonus;
            _Bonuses[(int)Feeling.Fear] = fearBonus;
            _Bonuses[(int)Feeling.Tiredness] = tirednessBonus;
        }

        public float GetBonus(Feeling feeling)
        {
            return _Bonuses[(int)feeling];
        }

        public void CreateInstance(Item item)
        {
            GameObject instance = PrefabManager.CreateInstance(_PrefabPath);
            SceneItem sceneItem = instance.GetComponent<SceneItem>();
            if (sceneItem)
            {
                sceneItem.Item = item;
            }
        }

        public void DestroyInstance()
        {
            if (!_Owner) return;
            _Owner.DestroyInstance();
        }

    }
}
