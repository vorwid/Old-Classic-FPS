using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Sprite IdleGun;
    public Sprite ShotGun;
    public float GunDamage;
    public float GunRange;
    public AudioClip ShotSound;
    public AudioClip ReloadSound;
    public AudioClip EmptyGunSound;
    public GameObject BulletHole;

    public Text AmmoText;

    public int AmmoAmount;
    public int AmmoClipSize;
    int AmmoLeft;
    int AmmoClipLeft;

    bool isShot;
    bool isReloading;

    AudioSource source;

    private void Awake()
    {

        source = GetComponent<AudioSource>();
        AmmoLeft = AmmoAmount;
        AmmoClipLeft = AmmoClipSize;
    }

    private void Update()
    {
        AmmoText.text = AmmoClipLeft + " / " + AmmoLeft;

        if (Input.GetButtonDown("Fire1") && isReloading == false)
            isShot = true;

        if (Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            Reload();
        }
    }


    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (isShot == true && AmmoClipLeft > 0 && isReloading == false)
        {
            isShot = false;
            AmmoClipLeft--;
            source.PlayOneShot(ShotSound);
            StartCoroutine("shot");  //wywolujemy korutyne
            if(Physics.Raycast(ray, out hit, GunRange))
            {
                Debug.Log("Wszedlem w kolizje z " + hit.collider.gameObject.name);
                hit.collider.gameObject.SendMessage("GunHit", GunDamage, SendMessageOptions.DontRequireReceiver);                 //zdalne wywolanie funkcji w tym obiekcie
                Instantiate(BulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)); 
            }
        }
        else if (isShot == true && AmmoClipLeft <= 0 && isReloading == false)
        {
            isShot = false;
            Reload();
        }
    }

    void Reload()
    {
        int BulletstoReload = AmmoClipSize - AmmoClipLeft;
        if(AmmoLeft >= BulletstoReload)
        {
            StartCoroutine("ReloadWeapon");
            AmmoLeft -= BulletstoReload;
            AmmoClipLeft = AmmoClipSize;
        }
        else if (AmmoLeft < BulletstoReload && AmmoLeft > 0)
        {
            StartCoroutine("ReloadWeapon");
            AmmoClipLeft += AmmoLeft;
            AmmoLeft = 0;
        }
        else if (AmmoLeft <= 0)
        {
            source.PlayOneShot(EmptyGunSound);
        }
    }

    IEnumerator ReloadWeapon()
    {
        isReloading = true;
        source.PlayOneShot(ReloadSound);
        yield return new WaitForSeconds(2);
        isReloading = false;
    }

    IEnumerator shot()
    {
        GetComponent<SpriteRenderer>().sprite = ShotGun;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = IdleGun;
    }
}
