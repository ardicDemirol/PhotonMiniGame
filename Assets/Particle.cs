using UnityEngine;
using Photon.Pun;
using System.Collections;

public class Particle : MonoBehaviour
{
    private WaitForSeconds waitTime = new(1f);
    void Start()
    {
        StartCoroutine(DestroyParticle());
    }

    void Update()
    {
        
    }

    IEnumerator DestroyParticle()
    {
        yield return waitTime;
        PhotonNetwork.Destroy(gameObject);
    }
}
