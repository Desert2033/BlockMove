using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField] private Transform _pointStart;
    [SerializeField] private Transform _pointEnd;
    [SerializeField] private Stack[] _stacksOnRow;
    
    private Vector3[] _cells;
    private readonly float _spacingCell = 1f;

    public const int MaxCells = 7;

    public Stack[] StacksOnRow => _stacksOnRow;

    private void Start()
    {
         if(_stacksOnRow.Length == 0)
            _stacksOnRow = new Stack[MaxCells];

        _cells = new Vector3[MaxCells];

        Vector3 cellPosition = _pointStart.position;

        for (int i = 0; i < MaxCells; i++)
        {
            _cells[i] = cellPosition;
            cellPosition.z += _spacingCell;
        }
    }

    private bool FindFreeCell(out int indexFreeStack)
    {
        for (int i = MaxCells - 1; i >= 0; i--)
        {
            if(_stacksOnRow[i] == null)
            {
                indexFreeStack = i;
                return true;
            }
        }

        indexFreeStack = -1;

        return false;
    }

    public bool TryAddStack(Stack stack, StackMover stackMove)
    {
        if (FindFreeCell(out int indexFreeStack))
        {
            stack.transform.position = _pointStart.position;

            stackMove.Move(_cells[indexFreeStack]);

            _stacksOnRow[indexFreeStack] = stack;

            return true;
        }

        return false;
    }

}
