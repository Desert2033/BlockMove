using UnityEngine;
using System.Collections.Generic;

public class SpawnStack : SpawnBase
{
    [SerializeField] private Transform _stackPosition;
    [SerializeField] private Stack _stackPrefab;
    [SerializeField] private List<Donut> _donutPrefabs;

    public override Stack Spawn()
    {
        int countLayers = Random.Range(Stack.MinLayers, Stack.MaxLayers + 1);

        Stack stack = Instantiate(_stackPrefab) as Stack;

        stack.transform.position = _stackPosition.position;

        for (int i = 0; i < countLayers; i++)
        {
            Donut donutPrefab = _donutPrefabs[Random.Range(0, _donutPrefabs.Count)];

            if (i + 1 == Stack.MaxLayers)
            {
                if (stack.BottomDonut.Colour == donutPrefab.Colour && stack.CenterDonut.Colour == donutPrefab.Colour)
                {
                    List<Donut> prefabsWithoutRepeat = new List<Donut>();

                    prefabsWithoutRepeat =
                        _donutPrefabs.FindAll(item => item.Colour != donutPrefab.Colour);

                    donutPrefab = prefabsWithoutRepeat[Random.Range(0, _donutPrefabs.Count)];
                }
            }

            Donut donut = Instantiate(donutPrefab) as Donut;

            stack.AddLayer(donut);
        }

        return stack;
    }
}