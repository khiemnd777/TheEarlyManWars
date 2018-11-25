using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectDisplayList : MonoBehaviour
{
    public List<ObjectDisplay> list;

    public void Add (ObjectDisplay objectDisplay)
    {
        list.Add (objectDisplay);
    }

    public void Remove (ObjectDisplay objectDisplay)
    {
        list.Remove (objectDisplay);
    }
}
