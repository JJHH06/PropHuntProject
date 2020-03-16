using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CambioO : MonoBehaviour, IPunObservable
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public float distancia = 4f;
    public GameObject chair;
    public GameObject fire;
    public GameObject parkBench;
    public GameObject thirdCaracter;
    public GameObject plant;
    public GameObject lamp;
    public GameObject computer;
    public GameObject car;
    public GameObject vending;
    private CapsuleCollider playerCollider;



    private bool isMine;



    public Text text;
    public bool isNotProp = true;


    RaycastHit hitInfo;
    void Start()
    {
        chair.SetActive(false);
        fire.SetActive(false);
        parkBench.SetActive(false);
        plant.SetActive(false);
        lamp.SetActive(false);
        computer.SetActive(false);
        car.SetActive(false);
        vending.SetActive(false);

        playerCollider = GetComponent<CapsuleCollider>();



        isMine = GetComponent<PhotonView>().IsMine;



        thirdCaracter.SetActive(true);



    }

    // Update is called once per frame
    // Update is called once per frame
    [System.Obsolete]
    void FixedUpdate()
    {
        if (!isMine)
            return;

        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(myRay, out hitInfo, distancia))
            {

                if (hitInfo.collider.gameObject.CompareTag("ObjFireExtin") || hitInfo.collider.gameObject.CompareTag("ObjChair") || hitInfo.collider.gameObject.CompareTag("ObjParkBench") || hitInfo.collider.gameObject.CompareTag("ObjPlant") || hitInfo.collider.gameObject.CompareTag("ObjLamp") || hitInfo.collider.gameObject.CompareTag("ObjComputer") || hitInfo.collider.gameObject.CompareTag("ObjCar") || hitInfo.collider.gameObject.CompareTag("ObjVending"))
                {
                    if (isNotProp)
                    {
                        isNotProp = false;
                        thirdCaracter.SetActive(false);
                        playerCollider.radius = 0.1f;
                        playerCollider.height = 0.2f;
                        playerCollider.center = new Vector3(0, 0, 0);



                    }
                    if (hitInfo.collider.gameObject.CompareTag("ObjFireExtin"))
                    {
                        fire.SetActive(true);
                        chair.SetActive(false);
                        parkBench.SetActive(false);
                        plant.SetActive(false);
                        lamp.SetActive(false);
                        computer.SetActive(false);
                        car.SetActive(false);
                        vending.SetActive(false);



                        text.text = "Objeto: Fire Extinguisher";
                    }
                    else if (hitInfo.collider.gameObject.CompareTag("ObjPlant"))
                    {
                        fire.SetActive(false);
                        chair.SetActive(false);
                        parkBench.SetActive(false);
                        lamp.SetActive(false);
                        plant.SetActive(true);
                        computer.SetActive(false);
                        car.SetActive(false);
                        vending.SetActive(false);



                        text.text = "Objeto: Planta";
                    }
                    else if (hitInfo.collider.gameObject.CompareTag("ObjChair"))
                    {
                        fire.SetActive(false);
                        chair.SetActive(true);
                        parkBench.SetActive(false);
                        plant.SetActive(false);
                        lamp.SetActive(false);
                        computer.SetActive(false);
                        car.SetActive(false);
                        vending.SetActive(false);



                        text.text = "Objeto: Chair";
                    }
                    else if (hitInfo.collider.gameObject.CompareTag("ObjParkBench"))
                    {
                        fire.SetActive(false);
                        chair.SetActive(false);
                        parkBench.SetActive(true);
                        plant.SetActive(false);
                        lamp.SetActive(false);
                        computer.SetActive(false);
                        car.SetActive(false);
                        vending.SetActive(false);



                        text.text = "Objeto: Park Bench";
                    }

                    else if (hitInfo.collider.gameObject.CompareTag("ObjLamp"))
                    {
                        fire.SetActive(false);
                        chair.SetActive(false);
                        parkBench.SetActive(false);
                        plant.SetActive(false);
                        lamp.SetActive(true);
                        computer.SetActive(false);
                        car.SetActive(false);
                        vending.SetActive(false);



                        text.text = "Objeto: Lampara";
                    }

                    else if (hitInfo.collider.gameObject.CompareTag("ObjComputer"))
                    {
                        fire.SetActive(false);
                        chair.SetActive(false);
                        parkBench.SetActive(false);
                        plant.SetActive(false);
                        lamp.SetActive(false);
                        computer.SetActive(true);
                        car.SetActive(false);
                        vending.SetActive(false);





                        text.text = "Objeto: Computadora";
                    }

                    else if (hitInfo.collider.gameObject.CompareTag("ObjCar"))
                    {
                        fire.SetActive(false);
                        chair.SetActive(false);
                        parkBench.SetActive(false);
                        plant.SetActive(false);
                        lamp.SetActive(false);
                        computer.SetActive(false);
                        car.SetActive(true);
                        vending.SetActive(false);






                        text.text = "Objeto: Mini Van";
                    }

                    else if (hitInfo.collider.gameObject.CompareTag("ObjVending"))
                    {
                        fire.SetActive(false);
                        chair.SetActive(false);
                        parkBench.SetActive(false);
                        plant.SetActive(false);
                        lamp.SetActive(false);
                        computer.SetActive(false);
                        car.SetActive(false);
                        vending.SetActive(true);






                        text.text = "Objeto: Vending Machine";
                    }








                }

            }

        }





    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            bool[] estados = { 
                chair.activeInHierarchy, 
                fire.activeInHierarchy, 
                parkBench.activeInHierarchy, 
                plant.activeInHierarchy, 
                lamp.activeInHierarchy, 
                computer.activeInHierarchy,
                car.activeInHierarchy, 
                vending.activeInHierarchy,
                thirdCaracter.activeInHierarchy
            
            };

            stream.SendNext(estados);

        } else if (stream.IsReading)
        {
            bool[] estados = (bool[])stream.ReceiveNext();

            
            chair.SetActive(estados[0]);
            fire.SetActive(estados[1]);
            parkBench.SetActive(estados[2]);
            plant.SetActive(estados[3]);
            lamp.SetActive(estados[4]);
            computer.SetActive(estados[5]);
            car.SetActive(estados[6]);
            vending.SetActive(estados[7]);
            thirdCaracter.SetActive(estados[8]);

        }

    }
}