using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Giris Paneli")]
    public GameObject GirisPanel;
    public InputField Oyuncuİsimİnput;

    [Header("Secim Paneli")]
    public GameObject SecimPanel;

    [Header("Oda Olusturma Paneli")]
    public GameObject OdaOlusturPanel;
    public InputField OdaAdiİnput;
    public InputField Ozellik_1_Input_key;
    public InputField Ozellik_1_Input_value;
    public InputField Ozellik_2_Input_key;
    public InputField Ozellik_2_Input_value;

    [Header("Genel tanımlamalar")]
    public GameObject OdalistePanel;
    public Text OdalistesiText;

    public GameObject OdaicPanel;    
    public Text OdainfobilgileriText;

    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;      
       
    }

    public override void OnConnectedToMaster()
    {

        SetActivePanel(SecimPanel.name);

    }
    public override void OnLeftRoom()
    {
        SetActivePanel(SecimPanel.name);

    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);
    }
    public override void OnJoinedRoom()
    {
        SetActivePanel(OdaicPanel.name);

        OdainfobilgileriText.text = "Odanın Adı : " + PhotonNetwork.CurrentRoom.Name + " | Odayı kuran kişi :  " + PhotonNetwork.MasterClient.NickName;
    }
    public void GeriDon()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();

        }
        SetActivePanel(SecimPanel.name);
    }

    public void odadanCik()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OdaOlustur()
    {
        

    }          

    public void GirisYap()
    {
        string playerName = Oyuncuİsimİnput.text;

        if (!playerName.Equals(""))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();

        }
        else
        {

            Debug.LogError("Oyuncu isimi tanımsız");
        }



    }

    public void OdaOzellikListesi()
    {      

        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();

        }
        SetActivePanel(OdalistePanel.name);
       
    }

    public void SetActivePanel(string activePanel)
    {
        GirisPanel.SetActive(activePanel.Equals(GirisPanel.name));
        SecimPanel.SetActive(activePanel.Equals(SecimPanel.name));
        OdaOlusturPanel.SetActive(activePanel.Equals(OdaOlusturPanel.name));       
        OdalistePanel.SetActive(activePanel.Equals(OdalistePanel.name));
        OdaicPanel.SetActive(activePanel.Equals(OdaicPanel.name));      
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        

    }

  
}
