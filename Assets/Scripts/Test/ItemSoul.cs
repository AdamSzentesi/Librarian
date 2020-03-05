using UnityEngine;

public class ItemSoul
{
    private int _ID;
    private string _Name;
    private Sprite _Icon;

    public ItemSoul(int ID, string name, Sprite icon)
    {
        _ID = ID;
        _Name = name;
        _Icon = icon;
    }

    public void Spawn(Vector3 position)
    {
        GameObject item = Librarian.PrefabManager.Instantiate(_ID);
        item.transform.position = position;
        item.name = _Name;
        ItemBody itemBody = item.GetComponent<ItemBody>();
        if (itemBody) itemBody.Init(this);
        item.SetActive(true);
    }

}
