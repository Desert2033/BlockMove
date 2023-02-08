using UnityEngine;

public class SpawnStackForTest : SpawnBase
{
    [SerializeField] private Transform _stackPosition;
    [SerializeField] private Stack _stackPrefabForTest;

    public override Stack Spawn()
    {
        Stack stack = Instantiate(_stackPrefabForTest) as Stack;

        stack.transform.position = _stackPosition.position;

        return stack;
    }
}
