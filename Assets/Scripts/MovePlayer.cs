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
		OSCHandler.Instance.SendMessageToClient ("pd", "/unity/trigger", "ready");
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/tempo", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/playseq", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/startWaitSound", 1);
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

        //Debug.Log(rb.position.x);

        float xPos = rb.position.x;
        float zPos = rb.position.z;
        xPos = (xPos + 9.25f) / (18.5f);
        zPos = (zPos + 9.25f) / (18.5f);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/xPos", xPos);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/zPos", zPos);

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

        if (Input.anyKey && keyPressed == false)
        {
			keyPressed = true;
			OSCHandler.Instance.SendMessageToClient("pd", "/unity/startWaitSound", 0);
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/stopWaitSound", 1);
			OSCHandler.Instance.SendMessageToClient("pd", "/unity/playseq", 1);
            //Debug.Log("A key or mouse click has been detected");
        }
        //*************
    }
		

	void OnTriggerEnter(Collider other) 
    {
        //Debug.Log("-------- COLLISION!!! ----------");

        if (other.gameObject.CompareTag ("Pick Up")) 
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			setCountText ();


            // change the tempo of the sequence based on how many obejcts we have picked up.
            if(count < 2)
            {
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/tempo", 1);
            }
            if (count < 4)
            {
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/tempo", 2);
            }
            else if(count < 6)
            {
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/tempo", 3);
            }
            else if (count < 8)
            {
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/tempo", 5);
            }
			else{ // if (count == 8){
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/playseq", 0); // stop background music
				OSCHandler.Instance.SendMessageToClient("pd", "/unity/winner", 1); //play victory sound on 8 coins
            }

        }
        else if(other.gameObject.CompareTag("Wall"))
        {
            //Debug.Log("-------- HIT THE WALL ----------");
            // trigger noise burst whe hitting a wall.
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/colwall", 1);
        }

    }

	void setCountText()
	{
		countText.text = "Count: " + count.ToString ();

		//************* Send the message to the client...
		OSCHandler.Instance.SendMessageToClient ("pd", "/unity/trigger", count);
		//*************
	}
		
}
