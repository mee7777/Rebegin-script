using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
    public int id;
    public bool isNpc;
    void Updata()
    {
        if (id == 4)
        {
            id = -999;
        }
    }
}
