using UnityEngine;

public class ItemBody : MonoBehaviour
{
    public Sprite Icon;
    //public string Name;

    private ItemSoul _ItemSoul;
    private bool _IsInitialized = false;

    public void Init(ItemSoul itemSoul)
    {
        _ItemSoul = itemSoul;
        _IsInitialized = true;
    }

    private void Start()
    {
        if (!_IsInitialized)
        {
            int ID = Librarian.PrefabManager.RegisterPrefab(GetType(), gameObject);
            Debug.Log("ID " + gameObject + " " + ID);
            _ItemSoul = new ItemSoul(ID, gameObject.name, Icon);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (_ItemSoul != null)
            {
                _ItemSoul.Spawn(new Vector3());
            }
        }
    }

}
