using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    public GameObject Player;
    public Camera cam;
    public float angle;

    public SpriteRenderer weaponSprite;

    public SpriteRenderer[] weaponSprites; //1-sword, 2-axe, 3-pickaxe
    public GameObject[] weapons; //1-sword, 2-axe, 3-pickaxe

    public Collider triggerColl;
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - Player.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = ClampHomemade(angle, 0, -90, 180);
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
        
        FixFacing();
        CheckIfUseWeapon();
        
    }

    public void SetActiveWeapon(int id)
    {
        for(int i = 0; i < 3; i++)
        {
            weapons[i].SetActive(false);
        }
        if(id == 0)
        {
            return;
        }
        weapons[id-1].SetActive(true);
        weaponSprite = weaponSprites[id-1];
        triggerColl = weapons[id-1].GetComponent<Collider>();
    }

    private void FixFacing()
    {
        if(angle > 90 || angle < -90)
        {
            if(!triggerColl.enabled)
                weaponSprite.flipX = true;
        }
        else 
        {
            if(!triggerColl.enabled)
                weaponSprite.flipX = false;
        }
    }
    private float ClampHomemade(float angle_, float min_, float min_2, float max_)
    {
        if(angle_ < min_ && angle_ > min_2)
            angle_ = min_;
        else if(angle_ > max_ || angle_ < min_2)
            angle_ = max_;

        return angle_;
    }
    private void CheckIfUseWeapon()
    {
        if(Input.GetMouseButtonDown(0))
        {
            int whereIsFacing = weaponSprite.flipX ? 1 : -1;
            triggerColl.enabled = true;
            weaponSprite.gameObject.transform.Rotate(0, 0, 45 * whereIsFacing);
        }
        else if(Input.GetMouseButtonUp(0))
        {   
            triggerColl.enabled = false;
            weaponSprite.gameObject.transform.eulerAngles = new Vector3(0, 0, angle - 90f);
        }
    }
}
