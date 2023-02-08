using UnityEngine;

public class Donut : MonoBehaviour
{
    [SerializeField] private DonutColours _colour;

    public DonutColours Colour => _colour;
}
