using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeManager : MonoBehaviour
{
    public Animator lightAnim;
    public Animator skyAnim;
    public Animator clockAnim;

    private float speed;
    public float dayDuration;
    private float dayElapsed;

    public int currentDay;
    public bool isNight;

    public TMP_Text currentDayTXT;
    // Start is called before the first frame update
    void Start()
    {
        dayElapsed = dayDuration/4;
        currentDay = 1;
        speed = 1/dayDuration;

        currentDayTXT.text = "Day " + currentDay.ToString();

        lightAnim.SetFloat("speed", speed);
        skyAnim.SetFloat("speed", speed);
        clockAnim.SetFloat("speed", speed);
    }

    // Update is called once per frame
    void Update()
    {
        dayElapsed += Time.deltaTime;
        if(dayElapsed >= dayDuration)
        {
            dayElapsed = 0;
            currentDay ++;

            currentDayTXT.text = "Day " + currentDay.ToString();
        }
        
        if(clockAnim.gameObject.transform.localEulerAngles.z < 180)
        {
            isNight = true;
        }
        else isNight = false;
    }
}
