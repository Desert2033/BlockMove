using UnityEngine;

public enum LevelState 
{
    SelectedRow,
    DonutMerge
}

public class Level : MonoBehaviour
{
    [SerializeField] private SelectedInputRoute _input;
    [SerializeField] private SpawnBase _spawnStack;
    [SerializeField] private Map _map;
    [SerializeField] private MergeSystem _mergeSystem;

    private LevelState _currentState;

    private void OnEnable()
    {
        StartSelectedRow();
    }

    private void ChangeState(LevelState levelState)
    {
        _currentState = levelState;

        switch (_currentState)
        {
            case LevelState.SelectedRow:
                _input.gameObject.SetActive(true);

                _mergeSystem.gameObject.SetActive(false);

                _map.AddNewStack(_spawnStack.Spawn());
                break;
            case LevelState.DonutMerge:
                _input.gameObject.SetActive(false);

                _mergeSystem.gameObject.SetActive(true);
                break;
        }
    }

    public void StartDonutMerge()
    {
        ChangeState(LevelState.DonutMerge);
    }

    public void StartSelectedRow()
    {
        ChangeState(LevelState.SelectedRow);
    }
}
