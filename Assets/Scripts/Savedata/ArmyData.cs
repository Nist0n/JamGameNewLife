using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ArmyData
{
    public List<GameObject> Army;

    public ArmyData(OurHand ourHand)
    {
        Army = ourHand.Army;
    }
}
