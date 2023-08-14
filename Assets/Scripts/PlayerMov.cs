using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerMov : MonoBehaviour
{
    public GameObject Bag, Win, Lose, Button;

    public InputControl inputcontrol;
    public TextMeshProUGUI score;
    public List<GameObject> cheesez;
    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
    void Start()
    {

        //Game Starts
        Time.timeScale = 1;
    }
    void OnTriggerEnter(Collider other)
    {
        //Handling different collisions
        if (other.tag == "Cheese")
        {
            other.transform.SetParent(Bag.transform);
            other.transform.localScale *= 0.5f;

            cheesez.Add(other.gameObject);
            //Set Cheese at back
            other.transform.localPosition = new Vector3(0f, 0.4f * cheesez.Count, 0f);
        }
        try
        {
            if (other.tag == "SmallObs")
            {
                Destroy(other.gameObject);

                Destroy(cheesez[cheesez.Count - 1]);
                cheesez.RemoveAt(cheesez.Count - 1);

            }

            if (other.tag == "BigObs")
            {
                Destroy(other.gameObject);
                foreach (var item in cheesez)
                {
                    Destroy(item);
                }
                cheesez.Clear();

            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            //I used exception here because im lazy and I know code above is going to crash when player hits an 
            //obstacle with empty sack, so lose screen activates here on the catch block. 
            Time.timeScale = 0;
            Button.SetActive(true);
            Lose.gameObject.SetActive(true);
            score.gameObject.SetActive(true);
            score.text += "0";
        }
        if (other.tag == "Wall")
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 7f, ForceMode.Impulse);
        }

        if (other.tag == "Finish")
        {
            inputcontrol.speed = 0;
            //I had to change time.timescale implementation because particle effects werent showing up
            //since game totally stops when timescale is 0.
            // Time.timeScale = 0;
            Button.SetActive(true);
            score.gameObject.SetActive(true);
            if (cheesez.Count > 0)
            {
                GetComponentInChildren<ParticleSystem>().Play();
                Win.gameObject.SetActive(true);
                score.text += cheesez.Count;
            }
            else
            {
                Lose.gameObject.SetActive(true);
                score.text += "0";
            }


        }

    }
}
