using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintController : MonoBehaviour
{
    bool MensajeActivo = false;
    bool PanelActivo = false;
    public Text texthint = null;
    public GameObject panel = null;

    private HintsWeaponInfo hintsWeaponInfo;
    public GameObject hintCanvas;

    public TextAsset AutomaticHintsWeaponTexts;

    public string mensaje;
    int indexLetra;
    public float frecuenciaLetras = 0.5f;
    float lastTime = 0;


    public static HintController instance = null; // se crea una variable estatica comun entre los gamecontrller a null

    void Awake()
    {
        if (instance == null)  //comprovamos si se ha creado un gamecontroler
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return; // cortamos la ejecucion 
        }

        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
       hintsWeaponInfo = JsonUtility.FromJson<HintsWeaponInfo>(AutomaticHintsWeaponTexts.text);
    }

    // Update is called once per frame
    void Update()
    {
        if (MensajeActivo == true && indexLetra < mensaje.Length)
        {
            if (Input.GetButtonDown("Submit"))
            {
                texthint.text = mensaje;
                indexLetra = mensaje.Length;

            }
            else if ((lastTime + frecuenciaLetras) < Time.realtimeSinceStartup)

            {
                lastTime = Time.realtimeSinceStartup;

                texthint.text = texthint.text + mensaje.Substring(indexLetra, 1);
                indexLetra++;




            }


        }
        else if (MensajeActivo == true)
        {
            if (Input.GetButtonDown("Submit"))
            {
                DeactivateHint();

            }


        }

    }

    public void ActivateHint()
    {
            MensajeActivo = true;
            PanelActivo = true;

            hintCanvas.SetActive(true);
            
            GameController.instance.PauseGame();
            indexLetra = 0;
            lastTime = 0;

       

    }


    public void DeactivateHint()
    {
       
        MensajeActivo = false;
        PanelActivo = false;

        
        hintCanvas.SetActive(false);

        GameController.instance.ResumeGame();
        lastTime = 0;
        texthint.text = "";

    }


    public void MostrarMensaje(string texto) 
    { 

        mensaje = texto;

        ActivateHint();
    
    }

    public void MostrarMensajePorArma(int index)
    {
        switch(index)
        {
        case 0:
                MostrarMensaje(hintsWeaponInfo.botas);
                break;

        case (int)WeaponController.TypeAmmo.PiercingAmmo:
            MostrarMensaje(hintsWeaponInfo.PiercingAmmo);
            break;
        case (int)WeaponController.TypeAmmo.heavyBullet:
            MostrarMensaje(hintsWeaponInfo.heavyBullet);
            break;
        case (int)WeaponController.TypeAmmo.bulletBoss:
            MostrarMensaje(hintsWeaponInfo.bulletBoss);
            break;
        case (int)WeaponController.TypeAmmo.bulletShield:
            MostrarMensaje(hintsWeaponInfo.bulletShield);
            break;
        default:
            break;
        }
    }
}
