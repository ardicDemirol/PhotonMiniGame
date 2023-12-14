using UnityEngine;
using Photon.Pun;
using System.Collections;

public class Particle : MonoBehaviourPunCallbacks
{
    private readonly WaitForSeconds _waitTime = new(1f);
    void Start()
    {
        StartCoroutine(DestroyParticle());
    }
    IEnumerator DestroyParticle()
    {
        yield return _waitTime;
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 10);
            PhotonNetwork.Destroy(gameObject);

            //other.transform.gameObject.GetComponent<PhotonView>().RpcSecure("TakeDamage", RpcTarget.All,true, 10);

        }
    }

}
