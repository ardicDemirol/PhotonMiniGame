using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System;

public class ServerManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI LogText;
    public TMP_InputField RoomName;
    public TMP_InputField Username;
    public GameObject LogInPanel;
    public GameObject PlayerListPanel;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Bağlantı koptu");
    }
    public override void OnConnectedToMaster()
    {

        Debug.Log("Server'e Bağlanıldı.");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {

        Debug.Log("Lobiye bağlanıldı.");
    }
    public override void OnJoinedRoom()
    {
        LogInPanel.SetActive(false);

        byte left = (byte)(PhotonNetwork.PlayerList.Length % 2);
        byte teamIndex;
        if (left == 1) teamIndex = 1;
        else teamIndex = 2;

        GameObject playerObject;
        if (teamIndex == 1)
        {
            playerObject = PhotonNetwork.Instantiate("PlayerBlue", Vector3.zero, Quaternion.identity);
        }
        else
        {
            playerObject = PhotonNetwork.Instantiate("PlayerRed", Vector3.zero, Quaternion.identity);
        }

        playerObject.GetComponent<PhotonView>().RPC("SetTeam", RpcTarget.All, teamIndex);

    }
    public override void OnLeftLobby()
    {
        Debug.Log("Lobiden Çıkıldı.");

    }
    public override void OnLeftRoom()
    {
        Debug.Log("Odadan Çıkıldı.");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Odaya girilemedi." + message + " - " + returnCode);

    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Random Odaya girilemedi." + message + " - " + returnCode);

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Oda oluşturulamadı." + message + " - " + returnCode);
    }

    public void CreateAndJoinRoom()
    {
        PhotonNetwork.NickName = Username.text;
        PhotonNetwork.JoinOrCreateRoom(RoomName.text, new RoomOptions { MaxPlayers = 3, IsOpen = true, IsVisible = true }, TypedLobby.Default);

    }

    public void RandomJoinRoom()
    {
        PhotonNetwork.NickName = Username.text;
        PhotonNetwork.JoinRandomRoom();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            PlayerListPanel.SetActive(true);
            PlayerListPanel.GetComponentInChildren<TextMeshProUGUI>().text = "";

            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if(player.IsMasterClient)
                {
                    PlayerListPanel.GetComponentInChildren<TextMeshProUGUI>().text += player.NickName + " - (Room )\n";
                }
                else
                {
                    PlayerListPanel.GetComponentInChildren<TextMeshProUGUI>().text += player.NickName + " - \n";
                }
            }
        }
        else
        {
            PlayerListPanel.SetActive(false);
        }
    }

}
