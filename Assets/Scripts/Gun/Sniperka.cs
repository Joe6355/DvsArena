using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniperka : MainGun
{
    public override void Shoot()
    {
        base.Shoot(); // ��������� �������
        PlaySound(sounds[0], destroyed: true);
    }
}
