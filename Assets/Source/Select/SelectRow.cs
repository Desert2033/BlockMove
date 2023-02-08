using UnityEngine;
using System;

[RequireComponent(typeof(Row))]
public class SelectRow : Selectable
{
    [SerializeField] private GameObject _selectObject;

    private Row _thisRow;

    public event Action<Row> OnSelected;

    private void OnEnable()
    {
        _thisRow = GetComponent<Row>();
    }

    public override void SelectView() 
    {
        _selectObject.SetActive(true);
    }

    public override void UnSelectView()
    {
        _selectObject.SetActive(false);
    }

    public override void Selected()
    {
        OnSelected?.Invoke(_thisRow);
    }
}
