using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private PhotonView _photonView;
    private Animator _animator;

    public TextMeshProUGUI UsernameText;
    public TextMeshProUGUI HealthText;

    public Slider HealthSlider;

    public GameObject FirePoint;


    public static readonly int IsRotateID = Animator.StringToHash("RotatePlayer");

    private float _health;


    private void Awake()
    {
        _health = 100f;
        HealthText.text = _health.ToString();
        _animator = GetComponent<Animator>();
        _photonView = GetComponent<PhotonView>();
        UsernameText.text = _photonView.Owner.NickName;
    }


    void Update()
    {
        if (!_photonView.IsMine) return;

        PlayerMoveController();
        AnimatorController();
    }

   

    private void AnimatorController()
    {
        if(Input.GetKey(KeyCode.B))
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

                Debug.Log(hit.transform.gameObject.tag);

            PhotonNetwork.Instantiate("FireVFX", FirePoint.transform.position, Quaternion.Euler(-90f,transform.eulerAngles.y,0f));

            //if (hit.transform.gameObject.TryGetComponent(out PhotonView photonView))
            //{
            //    photonView.RPC("TakeDamage", RpcTarget.All, 10);
            //}

            //hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 10);

        }
    }

    [PunRPC]
    void TakeDamage(int amount)
    {
        _health -= amount;
        HealthText.text = _health.ToString();
        HealthSlider.value = _health;

        if (_health <= 0) PhotonNetwork.Destroy(gameObject);
    }


}
