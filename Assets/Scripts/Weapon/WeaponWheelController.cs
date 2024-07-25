using UnityEngine;
using UnityEngine.UI;
public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    private bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;

    public WeaponFollow wf;

    void Update()
    {
        if(Input.GetKeyDown("1") || Input.GetKeyDown("2") || Input.GetKeyDown("3") || Input.GetKeyDown("q"))
        {
            weaponWheelSelected = !weaponWheelSelected;
            //Time.timeScale = weaponWheelSelected ? 0.7f : 1f;
        }

        if(weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);

            switch(weaponID) // ... (1)
            {
                case 0:
                    selectedItem.sprite = noImage;
                    wf.SetActiveWeapon(0);
                    break;
                case 1:
                    wf.SetActiveWeapon(1);
                    break;
                case 2:
                    wf.SetActiveWeapon(2);
                    break;
                case 3:
                    wf.SetActiveWeapon(3);
                    break;
            }

            if(Input.GetMouseButtonDown(0)) //ovo mora posle (1)
            {
                anim.SetBool("OpenWeaponWheel", false);
                weaponWheelSelected = false;
                //Time.timeScale = 1f;
            }
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }
            
    }
}

