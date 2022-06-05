using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class WeaponController : MonoBehaviour
{
    public float shootSpeed, shootTimer; // inicializamos velocidad de disparo y tiempo
    public Transform shootPos; //para ir cambiando la posicion
    public GameObject bullet; // la bala en si
    public GameObject bulletPircig;
    public GameObject heavyBullet;
    public GameObject bulletShield;
    public GameObject bulletBoss;

    // crear los game objetc de las ballas 
    public bool isShooting;
    public PlayerControler player;
  
    public int maxAmmo ;
    public Text ammoText;
    
    public enum TypeAmmo : int {
        BasicAmmo = 0,
        PiercingAmmo,
        heavyBullet,
        bulletBoss ,
        bulletShield ,
        Count

    }

    int currentAmmoIndex = (int)TypeAmmo.BasicAmmo;
    int[] municiones;
    bool[] unlockedAmmo;

    public void StartLevel() 
    {
        
        GameObject textGameObject;
        textGameObject = GameObject.Find("Canvas");
       
        Text[] childrens;
        childrens = textGameObject.GetComponentsInChildren<Text>(); //en hijos
        foreach (Text text in childrens)
        {
            if (text.gameObject.name == "Ammo") 
            {
                ammoText = text; 
            }
        }
    }

    void Start()
    {
        isShooting = false;

        municiones = new int[(int)TypeAmmo.Count];
        unlockedAmmo = new bool[(int)TypeAmmo.Count];
       
        for (int i = (int)TypeAmmo.BasicAmmo ; i < (int)TypeAmmo.Count; i++) 
        {

            municiones[i] = 0;
            unlockedAmmo[i] = false;
            
        }

        player = gameObject.GetComponent<PlayerControler>();
      
    }

    void Update()
    {

        bool gameStarted;

        gameStarted = player.GetPlayerStart();

        if (gameStarted) {

            SwichAmmo();

            if (Input.GetButtonDown("Fire1") && !isShooting)
            // si pulsamos fire1 "boton primario de raton"
            {
                FindObjectOfType<AudioManager>().Play("Disparo");
                StartCoroutine(Shoot());
                
            }

            IEnumerator Shoot()
            {

                if (currentAmmoIndex == (int)TypeAmmo.BasicAmmo || municiones[currentAmmoIndex] > 0 || GameController.instance.InfAmmo)
                {

                    isShooting = true;
                    GameObject bulletshoot = null;
                    //Debug.Log("Shoot"); comprobamos que funciona en la consola 

                    switch (currentAmmoIndex) // intentar cambiarlos a un swich
                    {

                        case ((int)TypeAmmo.BasicAmmo):
                            bulletshoot = bullet;
                            break;

                        case ((int)TypeAmmo.PiercingAmmo):
                            bulletshoot = bulletPircig;
                            break;

                        case ((int)TypeAmmo.heavyBullet):
                            bulletshoot = heavyBullet;
                            break;

                        case ((int)TypeAmmo.bulletBoss):
                            bulletshoot = bulletBoss;
                            break;

                        case ((int)TypeAmmo.bulletShield):
                            bulletshoot = bulletShield;
                            break;

                    }
    
   

                    GameObject newBullet = Instantiate(bulletshoot, shootPos.position, Quaternion.identity);
                    Debug.Log("POSICION DE RATON: " + Input.mousePosition);
                    Debug.Log("POSICION DE PLAYER: " + transform.position);

                    Vector3 camPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

                    camPoint.z = 0;

                    Vector3 directionShoot = camPoint - transform.position;
                    directionShoot = directionShoot.normalized;

                    Debug.Log("POSICION De raton fixed: " + camPoint);

                    newBullet.GetComponent<Bullet>().Init(new Vector2(directionShoot.x, directionShoot.y), shootSpeed);


                    yield return new WaitForSeconds(shootTimer); // espera unos segundos para poder volver a disparar 
                    isShooting = false;

                    if (!GameController.instance.InfAmmo) 
                    { 
                        AddAmmo(-1); 
                    }                    
                }            
                
            }
        }
    }

    public void AddAmmo(int ammoToAdd)
    {
        municiones[currentAmmoIndex] = municiones[currentAmmoIndex] + ammoToAdd;
        UpdateAmmoText();
    }



    public void AddAmmoFromAmmoIndex(int ammoToAdd , int ammoIndex)

    {

        
        if(!unlockedAmmo[ammoIndex] && ammoToAdd > 0)
        {
            unlockedAmmo[ammoIndex] = true;
            HintController.instance.MostrarMensajePorArma(ammoIndex);
        }

        municiones[ammoIndex] = municiones[ammoIndex] + ammoToAdd; 
        UpdateAmmoText();


    }



    private void UpdateAmmoText()
    {

        this.ammoText.text = "municion: " + $"{municiones[currentAmmoIndex]}";

        if (currentAmmoIndex == (int)TypeAmmo.BasicAmmo) 
        {
            this.ammoText.text =  "municion: \u221E";
        }

        switch (currentAmmoIndex)
        {
            case ((int)TypeAmmo.BasicAmmo):
                this.ammoText.text += " Basica";
                break;

            case ((int)TypeAmmo.PiercingAmmo):
                this.ammoText.text += " Penetrante";
                break;

            case ((int)TypeAmmo.heavyBullet):
                this.ammoText.text += " Rompe muros";
                break;

            case ((int)TypeAmmo.bulletBoss):
                this.ammoText.text += " Explosiva!";
                break;

            case ((int)TypeAmmo.bulletShield):
                this.ammoText.text += " Rompe Escudos";
                break;

        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        switch (collision.gameObject.tag)
        {

            case (("Ammo")):
                   AddAmmoFromAmmoIndex(collision.GetComponent<Ammo>().municion, (int)TypeAmmo.BasicAmmo); /// seteamos el valor de Addamo con los de municion
               collision.GetComponent<Ammo>().CollectAmmo(); // llamamos a una funcion de otra clase 
                break;

            case (("AmmoPirsing")):
                AddAmmoFromAmmoIndex(collision.GetComponent<Ammo>().municion, (int)TypeAmmo.PiercingAmmo);
                collision.GetComponent<Ammo>().CollectAmmo();
                break;

            case (("AmmoHeavy")):
                AddAmmoFromAmmoIndex(collision.GetComponent<Ammo>().municion, (int)TypeAmmo.heavyBullet);
                collision.GetComponent<Ammo>().CollectAmmo();
                break;

           case (("AmmoShield")):
                AddAmmoFromAmmoIndex(collision.GetComponent<Ammo>().municion, (int)TypeAmmo.bulletShield);
                collision.GetComponent<Ammo>().CollectAmmo();
                break;

            case (("AmmoBoss")):
                AddAmmoFromAmmoIndex(collision.GetComponent<Ammo>().municion, (int)TypeAmmo.bulletBoss);
                collision.GetComponent<Ammo>().CollectAmmo();
               break;

        }


    }

    public void SwichAmmo()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NextAmmo();
        }
        else if (Input.GetKeyDown(KeyCode.E)) 
        {
            PrevAmmo();
        }
    }

    private void NextAmmo() 
    {
        currentAmmoIndex++;
        if (currentAmmoIndex == (int)TypeAmmo.Count) 
        {
            currentAmmoIndex = (int)TypeAmmo.BasicAmmo; 
        }

        UpdateAmmoText();
    }

    private void PrevAmmo() 
    {
        currentAmmoIndex--;
        if (currentAmmoIndex == -1) 
        {
            currentAmmoIndex = (int)TypeAmmo.Count - 1 ;
        }

        UpdateAmmoText();
    }

    public void ResetAmmo() 
    {
     AddAmmo(maxAmmo);
     UpdateAmmoText();
    }   


}
