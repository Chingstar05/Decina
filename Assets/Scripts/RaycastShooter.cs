using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaycastShooter : MonoBehaviour
{
    public ParticleSystem flashEftect;                          //발사 이펙트 변수 선언

    public int magazineCapaclty = 30;                           //탄창의 크기

    private int currentAmmo;                                    //현재 총알의 갯수

    public TextMeshProUGUI  ammoUI;                               //총알 개수를 나타낼 TextMeshProUGUI 선언

    public Image reloadingUI;                                       //재장전 UI

    public float reloadTime = 2f;                                   //재장전 시간

    private float timer = 0;                                        //시간 확인용 타이머

    private bool isReloading = false;                               //재장전 확인용 bool 변수

    //사운드 출력 기능 변수 선언
    public AudioSource audioSource;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = magazineCapaclty;                              //현재 총알의 갯수를 탄창의 크기만큼으로 변경
        ammoUI.text = $"{currentAmmo}/{magazineCapaclty}";           //현재 총알의 갯수를 UI에 표시
        reloadingUI.gameObject.SetActive(false);                   //재장전 UI 비활성화
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && currentAmmo >0 && isReloading == false)                  //현재 총알이 0개 보다 많을때 조건 추가
        {
            audioSource.PlayOneShot(audioClip);
            flashEftect.Play();                                                      //이펙트 재생
            currentAmmo--;                                                           //총알 1개 소비
            ammoUI.text = $"{currentAmmo}/{magazineCapaclty}";                      //현재 총알 개수를 UI에 표시
            ShootRay();
        }
        if (Input.GetKeyDown(KeyCode.R))                     //R키를 누르면
        {
            isReloading = true;                             //재장전으로 변경
            reloadingUI.gameObject.SetActive(true);         //재장전 UI 비활성화
        }

        if (isReloading == true)                             //재장전중 일때
        {
            Reloading();                                    //Reloading 메서드 실행
        }
    }
    void ShootRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit))
        {
            Destroy(hit.collider.gameObject);
        }

    }
    void Reloading()
    {
        timer += Time.deltaTime;

        reloadingUI.fillAmount = timer / reloadTime;

        if(timer >= reloadTime)
        {
            timer = 0;
            isReloading = false;
            currentAmmo = magazineCapaclty;
            ammoUI.text = $"{currentAmmo}/{magazineCapaclty}";
            reloadingUI.gameObject.SetActive (false);
        }
    }
}
