using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Colours
{
    Yellow,
    Red,
    Blue,
    Green
}
public class Colour : MonoBehaviour
{
    [SerializeField]
    private Colours _objColor;

    private static Dictionary<Colours, Material> _dicColor;
    public event Action<Colours> _ColorChanged;
    
    public Colours objColor 
    { 
        get 
        { 
            return _objColor; 
        }
        set 
        {
            _objColor = value;
            Debug.Log($"Color changed to {value}");
            GetComponent<Renderer>().material = _dicColor[value];
            _ColorChanged?.Invoke(value);
        } 
    }
    
    private void Start()
    {
        if (_dicColor == null)
        {
            _dicColor = new Dictionary<Colours, Material>();
            _dicColor.Add(Colours.Red, Resources.Load("Red") as Material);
            _dicColor.Add(Colours.Yellow, Resources.Load("Yellow") as Material);
            _dicColor.Add(Colours.Blue, Resources.Load("Blue") as Material);
            _dicColor.Add(Colours.Green, Resources.Load("Green") as Material);
        }
    }
}
