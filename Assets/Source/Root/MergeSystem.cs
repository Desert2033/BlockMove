using UnityEngine;
using System.Collections.Generic;

public class MergeSystem : MonoBehaviour
{
    [SerializeField] private Map _map;

    private void Update()
    {
        /*if (TryFindFirstPriority(out MapCell cellStack))
        {
            Dictionary<Sides, MapCell> neighbors = _map.GetStackNeighbors(cellStack);

            foreach (var neighbor in neighbors)
            {
                if (Merge(cellStack.Stack, neighbor.Value.Stack))
                {
                    
                }
            }
        }*/
    }

    private bool Merge(Stack stackAddDonut, Stack stackRemoveDonut)
    {
        if (stackAddDonut != null && stackRemoveDonut != null)
        {
            if (stackAddDonut.HeadDonut.Colour == stackRemoveDonut.HeadDonut.Colour)
            {
                stackAddDonut.TakeDonut(stackRemoveDonut);
                stackRemoveDonut.RemoveLayer();

                return true;
            }
        }

        return false;
    }

    /*private bool TryFindFirstPriority(out MapCell cellStack)
    {
        foreach (var cell in _map.StackCells)
        {
            if (cell.Stack != null)
            {
                if (cell.Stack.TopDonut == null)
                {
                    if (cell.Stack.CenterDonut != null && cell.Stack.BottomDonut != null)
                    {
                        if (cell.Stack.CenterDonut.Colour == cell.Stack.BottomDonut.Colour)
                        {
                            cellStack = cell;

                            return true;
                        }
                    }
                }
            }
        }

        cellStack = default;

        return false;
    }*/

    /*private bool TryFindSecondPriority(out MapCell cellStack)
    {
        foreach (var cell in _map.StackCells)
        {
            if (cell.Stack != null)
            {
                if (cell.Stack.TopDonut == null && cell.Stack.CenterDonut == null)
                {
                    if (cell.Stack.BottomDonut != null)
                    {
                        cellStack = cell;

                        return true;
                    }
                }
            }
        }

        cellStack = default;

        return false;
    }*/
}
