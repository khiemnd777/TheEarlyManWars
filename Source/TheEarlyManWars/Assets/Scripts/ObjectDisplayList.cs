using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ObjectDisplayList : MonoBehaviour
{
    public List<ObjectDisplay> list;

    public void Remove (ObjectDisplay objectDisplay)
    {
        list.Remove(objectDisplay);
    }
}
