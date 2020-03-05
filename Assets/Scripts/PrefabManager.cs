using System;
using System.Collections.Generic;
using UnityEngine;

namespace Librarian
{
    public class PrefabManager
    {
        private static Dictionary<Type, int> _PrefabIndices = new Dictionary<Type, int>();
        private static List<GameObject> _Prefabs = new List<GameObject>();

        public static int RegisterPrefab(Type type, GameObject prefab)
        {
            int ID;

            if (_PrefabIndices.ContainsKey(type))
            {
                Debug.Log("OLD");
                ID = _PrefabIndices[type];
            }
            else
            {
                Debug.Log("NEW");
                ID = _Prefabs.Count;
                _PrefabIndices.Add(type, ID);

                GameObject copy = GameObject.Instantiate(prefab);
                copy.SetActive(false);
                _Prefabs.Add(copy);
            }
            
            return ID;
        }

        public static GameObject Instantiate(int ID)
        {
            return GameObject.Instantiate(_Prefabs[ID]);
        }

        // TODO: we could cache prefabs, might be faster, ore use assetbundles
        public static GameObject CreateInstance(string path)
        {
            Debug.Log("PATH " + path);
            return GameObject.Instantiate(Resources.Load<GameObject>(path));
        }



    }
}