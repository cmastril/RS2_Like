using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //Stats
    public int healthPoints;
    public int attackDamage;
    public int armor;
    public int speed;

    //Owner
    public enum Owner {player, enemy}
    public Owner owner = Owner.player;

    //Status
    public enum LifeStatus {alive, dead};
    public LifeStatus lifeStatus = LifeStatus.alive;
}
