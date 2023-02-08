using System.Collections.Generic;
using UnityEngine;
using System;

public enum Sides
{
    Left,
    Right,
    Top,
    Bottom
}

public class Map : MonoBehaviour
{
    [SerializeField] private Row[] _rows;

    private List<MapCell> _stackCells;
    private List<SelectRow> _selectedRows;
    private List<Stack> _allStacks;
    private Stack _newStack;

    public IEnumerator<Stack> AllStacks => _allStacks.GetEnumerator();

    public event Action OnSetStack;

    private void Start()
    {
        _selectedRows = new List<SelectRow>();
        _stackCells = new List<MapCell>();
        _allStacks = new List<Stack>();

        foreach (var row in _rows)
        {
            if(row.TryGetComponent(out SelectRow select))
            {
                _selectedRows.Add(select);
                select.OnSelected += SetStackOnRow;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var selectRow in _selectedRows)
        {
            selectRow.OnSelected -= SetStackOnRow;
        }
    }

    public void SetStackOnRow(Row row)
    {
        if (_newStack == null)
            throw new System.InvalidOperationException();


        if (row.TryAddStack(_newStack, _newStack.GetComponent<StackMover>()))
        {
            _stackCells.Add(new MapCell(_newStack, row));

            _allStacks.Add(_newStack);

            OnSetStack?.Invoke();
        }
    }

    public void AddNewStack(Stack newStack)
    {
        _newStack = newStack;
    }

    public Dictionary<Sides, Stack> GetStackNeighbors(Stack stack)
    {
        Dictionary<Sides, Stack> stackNeighbors = new Dictionary<Sides, Stack>();

        MapCell currentCell = _stackCells.Find(cell => cell.Stack == stack);

        int currentRowIndex = -1;
        int currentStackIndex = -1;

        for (int rowIndex = 0; rowIndex < _rows.Length; rowIndex++)
        { 
            if (currentCell.Row == _rows[rowIndex])
            {
                for (int stackIndex = 0; stackIndex < _rows[rowIndex].StacksOnRow.Length; stackIndex++)
                {
                    Stack currentStack = _rows[rowIndex].StacksOnRow[stackIndex];

                    if (currentStack == stack)
                    {
                        currentStackIndex = stackIndex;
                    }
                }

                currentRowIndex = rowIndex;
            }
        }

        int f = currentRowIndex - 1;

        /*_rows[f].StacksOnRow[0];

        stackNeighbors.Add(Sides.Bottom, GetStack(stackCell.Xindex, stackCell.Yindex - 1));
        stackNeighbors.Add(Sides.Top, GetStack(stackCell.Xindex, stackCell.Yindex + 1));
        stackNeighbors.Add(Sides.Left, GetStack(stackCell.Xindex - 1, stackCell.Yindex));
        stackNeighbors.Add(Sides.Right, GetStack(stackCell.Xindex + 1, stackCell.Yindex));
*/
        return stackNeighbors;
    }

    public struct MapCell
    {
        public Stack Stack { get; private set; }
        public Row Row { get; private set; }

        public MapCell(Stack stack, Row row)
        {
            Stack = stack;

            Row = row;
        }
    }
}