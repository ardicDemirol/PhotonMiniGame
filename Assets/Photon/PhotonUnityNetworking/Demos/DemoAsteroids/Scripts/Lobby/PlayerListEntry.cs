using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Photon.Pun;

public class PlayerListEntry : MonoBehaviour
    {
       
        public Text OyuncuAdi;        
        public Button OyuncuHazirbuton;
        public Image OyuncuSprite;
        private int oyuncuid;
        private bool Hazirmi;      
       
        public void Start()
        {
            
        }        
      

        public void Initialize(int playerId, string playerName)
        {
            oyuncuid = playerId;
            OyuncuAdi.text = playerName;
        }

       

        public void SetPlayerReady(bool playerReady)
        {
           
        }
    }
