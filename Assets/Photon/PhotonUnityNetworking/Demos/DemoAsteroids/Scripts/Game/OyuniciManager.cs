using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;

public class OyuniciManager : MonoBehaviourPunCallbacks
{
    public static OyuniciManager Instance = null;

    public Text InfoText;


    public void Awake()
    {
        Instance = this;
    }

    public override void OnEnable()
    {
        // CountdownTimer yapılacak
    }

    public void Start()
    {

    }

    public override void OnDisable()
    {
        // CountdownTimer yapılacak
    }




    #region PUN CALLBACKS

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Lobby");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Odadan Oyuncu Çıktı");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {

       // Oyuncu Özelliklerinde bir değişim olursa burada yakalanacak

    }

    #endregion



    private void StartGame()
    {
        Debug.Log("StartGame!");

      //  PhotonNetwork.Instantiate("OyuncuolusturmaObjesi", Vector3.zero, Quaternion.identity, 0);


    }
/*
    private bool CheckAllPlayerLoadedLevel()
    {
        // Oyuncular sahneye geldi mi gelmedi mi burada kontrol edilecek
    }
*/

    private void OnCountdownTimerIsExpired()
    {
       // geri sayım bittiğinde oyun başlatılacak
    }
}
