using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    protected Rigidbody rb;
    public int FireRate;
    public GameObject Projectile;
    private float cooldown = 0f;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }
    
    void Update(){
        rb.MovePosition(new Vector3(Input.GetAxis("LeftStickHorizontal") * Time.deltaTime, 0, Input.GetAxis("LeftStickVertical") * Time.deltaTime));
        transform.rotation = Quaternion.Euler(new Vector3(Input.GetAxis("RightStickHorizontal"), 0, Input.GetAxis("RightStickVertical")));
        cooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && cooldown <= 0f) {
            cooldown = FireRate;
            Instantiate(Projectile, rb.position, rb.rotation);
        }
    }
}
