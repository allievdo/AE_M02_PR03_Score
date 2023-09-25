using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneColor : MonoBehaviour
{
    public static event Action<ChangeSceneColor> OnChangeSceneColor;

    [SerializeField]
    private string _poiName2;

    public string PoiName2 { get { return _poiName2; } }

    private void OnTriggerEnter(Collider other)
    {
        if(OnChangeSceneColor != null)
        {  OnChangeSceneColor(this); }
    }
}
