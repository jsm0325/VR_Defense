using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieButton : MonoBehaviour
{
    public GameObject monster;
    public void dieButton()
    {
        monster.GetComponent<Monster>().TakeDamage(101);
    }
}
