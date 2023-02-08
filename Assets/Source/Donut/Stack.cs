using UnityEngine;
using System.Collections;

public class Stack : MonoBehaviour
{
    [SerializeField] private Transform _topLayer;
    [SerializeField] private Transform _centerLayer;
    [SerializeField] private Transform _bottomLayer;

    private Animator _thisAnimator;

    public Donut TopDonut { get; private set; }
    public Donut CenterDonut { get; private set; }
    public Donut BottomDonut { get; private set; }
    public Donut HeadDonut { get; private set; }


    public const int MaxLayers = 3;
    public const int MinLayers = 1;

    private void Start()
    {
        _thisAnimator = GetComponent<Animator>();

        if (_bottomLayer.childCount != 0)
        {
            BottomDonut = _bottomLayer.GetChild(0).GetComponent<Donut>();
            HeadDonut = BottomDonut;
        }
        if (_centerLayer.childCount != 0)
        {
            CenterDonut = _centerLayer.GetChild(0).GetComponent<Donut>();
            HeadDonut = CenterDonut;
        }
        if (_topLayer.childCount != 0)
        {
            TopDonut = _topLayer.GetChild(0).GetComponent<Donut>();
            HeadDonut = TopDonut;
        }
    }

    private Donut SetDonut(Donut donut, Transform layer)
    {
        donut.transform.position = layer.position;
        donut.transform.localScale = layer.localScale;
        donut.transform.SetParent(layer);

        return donut;
    }

    public bool IsAllDonutsOneColours(Donut donut)
    {
        int countRepeatColours = 0;

        if(TopDonut?.Colour == donut.Colour)
        {
            countRepeatColours++;
        }
        if (CenterDonut?.Colour == donut.Colour)
        {
            countRepeatColours++;
        }
        if (BottomDonut?.Colour == donut.Colour)
        {
            countRepeatColours++;
        }

        return countRepeatColours >= MaxLayers;
    }

    public void AddLayer(Donut donut)
    {
        if (_bottomLayer.childCount == 0)
        {
            BottomDonut = SetDonut(donut, _bottomLayer);
            HeadDonut = BottomDonut;
        }
        else if (_centerLayer.childCount == 0)
        {
            CenterDonut = SetDonut(donut, _centerLayer);
            HeadDonut = CenterDonut;
        }
        else if (_topLayer.childCount == 0)
        {
            TopDonut = SetDonut(donut, _topLayer);
            HeadDonut = TopDonut;
        }
        else
        {
            throw new System.Exception($"Stack can have only {MaxLayers} layers !!!");
        }
    }

    public void RemoveLayer()
    {
        HeadDonut.transform.parent = null;

        if (HeadDonut == TopDonut)
            HeadDonut = CenterDonut;
        else if (HeadDonut == CenterDonut)
            HeadDonut = BottomDonut;
        else
        {
            Die();
        }
    }

    public void TakeDonut(Stack stackGivingDonut)
    {
        Donut donutAdd = stackGivingDonut.HeadDonut;

        stackGivingDonut.RemoveLayer();

        AddLayer(donutAdd);

        if (IsAllDonutsOneColours(donutAdd))
        {
            Die();
        }
    }
    
    public void Die()
    {
        _thisAnimator.SetTrigger("Destroy");

        StartCoroutine(DieCoroutine());
    }

    public IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
