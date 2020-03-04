using UnityEngine;

[CreateAssetMenu(fileName = "TestItem", menuName = "Librarian/TestItem")]
public class TestItem : ScriptableObject
{
    public GameObject _Prefab;

    public bool _IsInitialized = false;
    
    public void Init(GameObject prefab)
    {
        if (prefab)
        {
            if (!_IsInitialized)
            {
                _Prefab = prefab;
                _IsInitialized = true;
            }
        }
        else
        {
            _IsInitialized = false;
        }

    }
}
