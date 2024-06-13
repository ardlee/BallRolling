using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//************** use UnityOSC namespace...
using UnityOSC;
using static System.Net.Mime.MediaTypeNames;
//*************

public class MovePlayer : MonoBehaviour {

	public float speed;
	public UnityEngine.UI.Text countText;

    private Rigidbody rb;
	private int count;
	private bool keyPressed;

    //************* Need to setup this server dictionary...
    Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog> ();
	//*************

	// Use this for initialization
	void Start () 
	{
        UnityEngine.Application.runInBackground = true; //allows unity to update when not in focus

        //************* Instantiate the OSC Handler...
        OSCHandler.Instance.Init ();
		OSCHandler.Instance.SendMessageToClient ("pd", "/unity/trigger", 1);
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/ballXpos", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/ballZpos", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/pointsCount", 1);
        //*************

        rb = GetComponent<Rigidbody> ();
		count = 0;
		setCountText ();
		keyPressed = false;
	}
	

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

        rb = GetComponent<Rigidbody> ();
        //Debug.Log(rb.position.x);
        float xVelo = Math.Abs(rb.velocity.x);
        float zVelo = Math.Abs(rb.velocity.z);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/ballXvelo", xVelo);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/ballZvelo", zVelo);

        float xPos = rb.position.x;
        float zPos = rb.position.z;
        xPos = (xPos + 9.25f) / (18.5f);
        zPos = (zPos + 9.25f) / (18.5f);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/ballXpos", xPos);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/ballZpos", zPos);

        Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

		rb.AddForce (movement*speed);

		//************* Routine for receiving the OSC...
		OSCHandler.Instance.UpdateLogs();
		Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
		servers = OSCHandler.Instance.Servers;

		foreach (KeyValuePair<string, ServerLog> item in servers) {
			// If we have received at least one packet,
			// show the last received from the log in the Debug console
			if (item.Value.log.Count > 0) {
				int lastPacketIndex = item.Value.packets.Count - 1;

				//get address and data packet
				countText.text = item.Value.packets [lastPacketIndex].Address.ToString ();
				countText.text += item.Value.packets [lastPacketIndex].Data [0].ToString ();

			}
		}
    }
		

	void OnTriggerEnter(Collider other) 
    {
        //Debug.Log("-------- COLLISION!!! ----------");

        if (other.gameObject.CompareTag ("Pick Up")) 
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			setCountText ();

            OSCHandler.Instance.SendMessageToClient("pd", "/unity/collect1", 1);
        }
        if (other.gameObject.CompareTag("pickup2"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            setCountText();
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/collect2", 1);
        }
        if (other.gameObject.CompareTag("pickup3"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            setCountText();
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/collect3", 1);
        }
        if (other.gameObject.CompareTag("pickup4"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            setCountText();
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/collect4", 1);
        }
        else if(other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("-------- HIT THE WALL ----------");
             //trigger noise burst whe hitting a wall.
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/colwall", 1);
        }
        
    }

	void setCountText()
	{
		countText.text = "Count: " + count.ToString ();

		//************* Send the message to the client...
		OSCHandler.Instance.SendMessageToClient ("pd", "/unity/pointsCount", count);
		//*************
	}
		
}
