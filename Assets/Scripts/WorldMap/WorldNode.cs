using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldNode : MonoBehaviour
{
    //Scene reference
    public string regionSceneName;

    //Nodes that this can travel to
    public WorldNode rightReference;
    public WorldNode leftReference;
    public WorldNode upReference;
    public WorldNode downReference;
}
