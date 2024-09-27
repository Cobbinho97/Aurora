using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysPlatform : MonoBehaviour
{
    public GameObject Player;

    public GameObject Platform;

    private void OnCollisionEnter2D(Collision2D collid)
    {
            Player.transform.parent = collid.gameObject.transform;
    }
    private void OnCollisionExit2D(Collision2D collid)
    {
            collid.transform.parent = null;
    }
}
