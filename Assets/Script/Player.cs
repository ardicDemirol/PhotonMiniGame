using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using Photon.Pun.Demo.PunBasics;
using Photon.Pun.UtilityScripts;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class Player : MonoBehaviourPunCallbacks
{
    private PhotonView _photonView;
    private Animator _animator;

    public TextMeshProUGUI UsernameText;
    public TextMeshProUGUI HealthText;
    public Slider HealthSlider;

    public GameObject FirePoint;

    private CameraWork _cameraWork;

    private readonly Hashtable _props = new()
    {
        {"Order", 1},
        {"Win", 0},
        {"Lose", 0}
    };


    public static readonly int IsRotateID = Animator.StringToHash("RotatePlayer");

    private float _health;


    private void Awake()
    {
        _health = 100f;
        HealthText.text = _health.ToString();
        _animator = GetComponent<Animator>();
        _photonView = GetComponent<PhotonView>();
        UsernameText.text = _photonView.Owner.NickName;
        _cameraWork = gameObject.GetComponent<CameraWork>();


    }

    private void Start()
    {
        //if(_cameraWork)
        //{
        //    if(_photonView.IsMine) _cameraWork.OnStartFollowing();
        //}

        PhotonNetwork.LocalPlayer.AddScore(0);

        PhotonNetwork.LocalPlayer.SetCustomProperties(_props);
    }

    void Update()
    {
        if (!_photonView.IsMine) return;

        PlayerMoveController();
        AnimatorController();
        SystemController();
    }

    private void SystemController()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Debug.Log("Score: " + PhotonNetwork.LocalPlayer.GetScore());
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            PhotonNetwork.LocalPlayer.SetScore(PhotonNetwork.LocalPlayer.GetScore() + 20);
            Debug.Log("Score Gıncellendi");
        }


        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Order", out object orderNumber);
            PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Win", out object winNumber);
            PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Lose", out object loseNumber);
            PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Prize", out object prizeNumber);

            Debug.Log("Order: " + orderNumber);
            Debug.Log("Win: " + winNumber);
            Debug.Log("Lose: " + loseNumber);
            Debug.Log("Prize: " + prizeNumber);
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            _props.TryGetValue("Order", out object orderNumber);
            _props["Order"] = (int)orderNumber + 1;

            _props.TryGetValue("Win", out object winNumber);
            _props["Win"] = (int)winNumber + 1;

            _props.TryGetValue("Lose", out object loseNumber);
            _props["Lose"] = (int)loseNumber + 1;

            PhotonNetwork.LocalPlayer.SetCustomProperties(_props);
            Debug.Log("UPDATED");
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            PhotonNetwork.LocalPlayer.CustomProperties.Remove("Win");
            _props.Remove("Win");
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            _props.Add("Prize", 5);
            PhotonNetwork.LocalPlayer.SetCustomProperties(_props);
            Debug.Log("New Property Added");
        }
        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                player.CustomProperties.TryGetValue("Order", out object orderNumber);
                player.CustomProperties.TryGetValue("Win", out object winNumber);
                Debug.Log("Player Name: " + player.NickName + "\n Order of Player : " + orderNumber + "\n Player Win Number: " + winNumber);

            }
        }


        if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(PhotonNetwork.LocalPlayer.GetPhotonTeam().ToString());
        }

    }

    private void AnimatorController()
    {
        if (Input.GetKey(KeyCode.B))
        {
            _animator.SetBool(IsRotateID, true);
        }
        else
        {
            _animator.SetBool(IsRotateID, false);
        }
    }

    private void PlayerMoveController()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            Vector3 dir = hit.point - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir * Time.deltaTime * 2f);
            Debug.DrawLine(transform.position, hit.point);

        }
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 20f;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * 20f;
        transform.Translate(x, 0, y);


        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray2, out hit))
            {
                PhotonNetwork.Instantiate("FireVFX", FirePoint.transform.position, Quaternion.Euler(-90f, transform.eulerAngles.y, 0f));
            }

            //if (hit.transform.gameObject.TryGetComponent(out PhotonView photonView))
            //{
            //    photonView.RPC("TakeDamage", RpcTarget.All, 10);
            //}

            //hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 10);
        }



    }

    [PunRPC]
    private void TakeDamage(int amount)
    {
        _health -= amount;
        HealthText.text = _health.ToString();
        HealthSlider.value = _health;

        if (_health <= 0) PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    private void SetTeam(byte index)
    {
        PhotonNetwork.LocalPlayer.JoinTeam(index);

    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
    {
        targetPlayer.CustomProperties.TryGetValue("Order", out object orderNumber);
        Debug.Log("Order has changed: " + orderNumber);

    }



}
