using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarshMallowManager : MonoBehaviour
{
    public Marshmallow marshfab;
    public GameObject dummyFab;

    public List<Marshmallow> boxMallows = new List<Marshmallow>();
    public List<Marshmallow> sceneMallows = new List<Marshmallow>();

    public int maxBoxMallows;
    public int maxSceneMallows;

    public List<Transform> marshPositions;
    public List<GameObject> dummyMarsh;

    public float spawnSpeed;


    private void Start()
    {
        int i = 0;
        foreach(Transform t in marshPositions)
        {
            GameObject dum = Instantiate(dummyFab, marshPositions[i].position, Quaternion.Euler(marshPositions[i].rotation.eulerAngles));
            dummyMarsh.Add(dum);
            DummyMarsh(i);
            ++i;
        }
    }

    void NewMarsh(int marshIndex)
    {
        Marshmallow m = Instantiate(marshfab, marshPositions[marshIndex].position, Quaternion.Euler(marshPositions[marshIndex].rotation.eulerAngles));
        m.mallowIndex = marshIndex;
        m.onGrab += ActivateMarsh;
        boxMallows.Add(m);
    }

    void DummyMarsh(int index)
    {
        //set active at position
        dummyMarsh[index].gameObject.SetActive(true);
        dummyMarsh[index].transform.position = marshPositions[index].position - new Vector3(0,.5f,0);

        StartCoroutine(MoveMarsh(dummyMarsh[index].transform, marshPositions[index],index));

    }

    public void ActivateMarsh(Marshmallow m)
    {
        if(boxMallows.Contains(m))
        {
            boxMallows.Remove(m);
            sceneMallows.Add(m);
            DummyMarsh(m.mallowIndex);
        }    
    }

    IEnumerator MoveMarsh(Transform t1,Transform t2, int index)
    {
        float time = 0;
        Vector3 start = t1.position;
        while(time < 1)
        {
            time += Time.deltaTime * spawnSpeed;
            t1.transform.position = Vector3.Lerp(start, t2.position, time);
            yield return null;
        }
        NewMarsh(index);
        dummyMarsh[index].gameObject.SetActive(false);
        

     
    }
}
