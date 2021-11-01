using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private AudioSource stay, run;

    private float _pith;
    private float _timing;

    private void Start()
    {
        _timing = Random.Range(1f, 10f);
    }
    public void Pitch(float realspeed, Vector2 speed)
    {
        _pith = realspeed*2/(speed.y + speed.x);
        stay.pitch = _pith;
        run.pitch = _pith;
    }

    public void Stay()
    {
        stay.PlayDelayed(_timing);
    }
    public void Run()
    {
        stay.Stop();
        run.PlayDelayed(_timing);
    }
    public void Dead()
    {
        stay.Stop();
        run.Stop();
    }


}
