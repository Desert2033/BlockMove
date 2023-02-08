using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public abstract void SelectView();

    public abstract void UnSelectView();

    public abstract void Selected();
}
