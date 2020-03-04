using UnityEngine;

public class TestThing : MonoBehaviour
{
    public TestItem Item;

    private void OnValidate()
    {
        Item.Init(gameObject);

    }

}
