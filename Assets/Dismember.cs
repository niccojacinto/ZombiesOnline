using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Dismember : NetworkBehaviour {

    int hits = 0;
    bool disemboweled = false;

    public void BulletHit()
    {
        if (disemboweled)
            return;
        if(hits > 3)
        {
            GetComponentInParent<AIController>().DisemBowel(gameObject.tag);
            disemboweled = true;
        }
        hits++;
    }
}
