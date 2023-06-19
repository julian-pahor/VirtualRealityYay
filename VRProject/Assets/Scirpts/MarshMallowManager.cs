using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarshMallowManager : MonoBehaviour
{
    public Marshmallow marshfab;
    public List<Marshmallow> boxMallows = new List<Marshmallow>();
    public List<Marshmallow> sceneMallows = new List<Marshmallow>();

    public int maxBoxMallows;
    public int maxSceneMallows;

    Vector3[] mushSpots;
    Vector3[] mushRots;


    private void Start()
    {
        mushSpots = new Vector3[boxMallows.Count];
        mushRots = new Vector3[boxMallows.Count];

        for(int i = 0; i < boxMallows.Count; i++)
        {
            boxMallows[i].mallowIndex = i;
            mushRots[i] = boxMallows[i].transform.rotation.eulerAngles;
            mushSpots[i] = boxMallows[i].transform.position;

            boxMallows[i].Initialise(mushSpots[i], this);
        }

       
    }

    void NewMarsh(int marshIndex)
    {
        Marshmallow m = Instantiate(marshfab, mushSpots[marshIndex] - new Vector3(0,.5f,0), Quaternion.Euler(mushRots[marshIndex]));
        m.onGrab += ActivateMarsh;
        boxMallows.Add(m);
    }

    public void ActivateMarsh(Marshmallow m)
    {
        if(boxMallows.Contains(m))
        {
            boxMallows.Remove(m);
            sceneMallows.Add(m);
            NewMarsh(m.mallowIndex);
        }    
    }
}
