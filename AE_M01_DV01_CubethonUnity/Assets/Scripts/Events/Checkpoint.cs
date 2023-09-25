using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static event Action<Checkpoint> OnCheckpoint;

    [SerializeField]
    private string _poiName;

    public string PoiName { get { return _poiName; } }
    private void OnTriggerEnter(Collider other)
    {
        if (OnCheckpoint != null)
        {
            OnCheckpoint(this);
        }
    }
}
