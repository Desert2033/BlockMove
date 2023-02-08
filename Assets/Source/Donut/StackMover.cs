using UnityEngine;

[RequireComponent(typeof(Stack))]
public class StackMover : MonoBehaviour, IMoveable
{
    [SerializeField] private float _speed;

    private Vector3 _finishPoint;
    private bool _isMove = false;

    private void Start()
    {
        _finishPoint = transform.position;
    }

    private void OnValidate()
    {
        if (_speed <= 0f)
            _speed = 1f;
    }

    private void Update()
    {
        Debug.Log(_isMove);

        if (_isMove)
        {
            transform.position =
                     Vector3.MoveTowards(transform.position, _finishPoint, Time.deltaTime * _speed);

            if (transform.position == _finishPoint)
                _isMove = false;
        }
    }
    public void Move(Vector3 finishPoint)
    {
        _finishPoint = finishPoint;

        _isMove = true;
    }
}
