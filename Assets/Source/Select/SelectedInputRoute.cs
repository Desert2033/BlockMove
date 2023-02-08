using UnityEngine;
using System.Collections;

public class SelectedInputRoute : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Selectable _currentSelect;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();

        _playerInput.Player.Selected.performed += context => SelectClick();
    }

    private void OnDisable()
    {
        _playerInput.Disable();

        _playerInput.Player.Selected.performed -= context => SelectClick();
    }

    public void SelectClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            GameObject hitObject = hit.collider.gameObject;
            
            if (hitObject.TryGetComponent(out Selectable selectable))
            {
                _currentSelect = selectable;

                _currentSelect.SelectView();

                StartCoroutine(SelectClickUnSelect());
            }
        }
    }

    private IEnumerator SelectClickUnSelect()
    {
        yield return new WaitForSeconds(0.1f);

        _currentSelect.UnSelectView();

        _currentSelect.Selected();

        _currentSelect = null;
    }
}
