using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Vector3 _animatorMoveDir;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat("SpeedFire", 1);
        PlayerGun.shotGunAnimation += ShotGunFire;
        PlayerGun.reload += Reload;
    }

    public void RunAnimation(Vector3 moveDir)
    {
        _animator.SetBool("Falling", false);
        _animatorMoveDir = transform.InverseTransformVector(moveDir);
        _animator.SetFloat("x", _animatorMoveDir.x);
        _animator.SetFloat("y", _animatorMoveDir.z);
    }

    public void Falling()
    {
        _animator.SetBool("Falling", true);
    }

    void ShotGunFire(float speedAnimation)
    {
        _animator.SetFloat("SpeedFire", speedAnimation);
        _animator.SetTrigger("ShotGunFire");
    }

    void Reload(float speed)
    {        
        _animator.SetTrigger("Reload Rifle");
    }
}
