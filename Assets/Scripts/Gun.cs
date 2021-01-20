using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Sprite IdleGun;
    public Sprite ShotGun;
    public float GunDamage;
    public float GunRange;
    public AudioClip ShotSound;
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetButtonDown("Fire1"))
        {
            source.PlayOneShot(ShotSound);
            StartCoroutine("shot");  //wywolujemy korutyne
            if(Physics.Raycast(ray, out hit, GunRange))
            {
                Debug.Log("Wszedlem w kolizje z " + hit.collider.gameObject.name);
                hit.collider.gameObject.SendMessage("GunHit", GunDamage, SendMessageOptions.DontRequireReceiver);                 //zdalne wywolanie funkcji w tym obiekcie
            }
        }
    }

    IEnumerator shot()
    {
        GetComponent<SpriteRenderer>().sprite = ShotGun;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = IdleGun;
    }
}
