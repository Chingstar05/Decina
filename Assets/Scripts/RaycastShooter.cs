using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaycastShooter : MonoBehaviour
{
    public ParticleSystem flashEftect;                          //�߻� ����Ʈ ���� ����

    public int magazineCapaclty = 30;                           //źâ�� ũ��

    private int currentAmmo;                                    //���� �Ѿ��� ����

    public TextMeshProUGUI  ammoUI;                               //�Ѿ� ������ ��Ÿ�� TextMeshProUGUI ����

    public Image reloadingUI;                                       //������ UI

    public float reloadTime = 2f;                                   //������ �ð�

    private float timer = 0;                                        //�ð� Ȯ�ο� Ÿ�̸�

    private bool isReloading = false;                               //������ Ȯ�ο� bool ����

    //���� ��� ��� ���� ����
    public AudioSource audioSource;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = magazineCapaclty;                              //���� �Ѿ��� ������ źâ�� ũ�⸸ŭ���� ����
        ammoUI.text = $"{currentAmmo}/{magazineCapaclty}";           //���� �Ѿ��� ������ UI�� ǥ��
        reloadingUI.gameObject.SetActive(false);                   //������ UI ��Ȱ��ȭ
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && currentAmmo >0 && isReloading == false)                  //���� �Ѿ��� 0�� ���� ������ ���� �߰�
        {
            audioSource.PlayOneShot(audioClip);
            flashEftect.Play();                                                      //����Ʈ ���
            currentAmmo--;                                                           //�Ѿ� 1�� �Һ�
            ammoUI.text = $"{currentAmmo}/{magazineCapaclty}";                      //���� �Ѿ� ������ UI�� ǥ��
            ShootRay();
        }
        if (Input.GetKeyDown(KeyCode.R))                     //RŰ�� ������
        {
            isReloading = true;                             //���������� ����
            reloadingUI.gameObject.SetActive(true);         //������ UI ��Ȱ��ȭ
        }

        if (isReloading == true)                             //�������� �϶�
        {
            Reloading();                                    //Reloading �޼��� ����
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
