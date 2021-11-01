using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroer : MonoBehaviour, IPooledObjects
{
    public PollerObject.ObjectInfo.ObjectType Type => type;
    [SerializeField] private PollerObject.ObjectInfo.ObjectType type;

    private float _currentTime = 0;

    private void Update()
    {
        if (_currentTime >= 0.5f)
        {
            _currentTime = 0;
            PollerObject.Instance.DestroyGameObject(this.gameObject);
        }
    }
}