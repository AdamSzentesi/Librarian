using UnityEngine;

namespace Librarian
{
    public class PrefabManager
    {
        // TODO: we could cache prefabs, might be faster, ore use assetbundles
        public static GameObject CreateInstance(string path)
        {
            Debug.Log("PATH " + path);
            return GameObject.Instantiate(Resources.Load<GameObject>(path));
        }
    }
}