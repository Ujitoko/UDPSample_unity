using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class UdpSender : MonoBehaviour {

   public int remotePort = 19800;
   public int localPort = 19801;
   public string name;
   public string IP;
   public string gameName;

   public string broadcastSig;
   public string broadcastMsg;

   protected bool refreshing;

   protected UdpClient sender;


	void OnEnable ()
	{
		sender = new UdpClient(localPort, AddressFamily.InterNetwork);
		IPEndPoint groupEP = new IPEndPoint(IPAddress.Broadcast, remotePort);
		sender.Connect(groupEP);
		IP = GetLocalIP();
		InvokeRepeating("SendData", 1, 5f);

	}


   void OnDisable()
   {
     if(sender != null)
       sender.Close();
   }


	protected void SendData()
	{
	    broadcastMsg = broadcastSig + " TIME: " + Time.time.ToString();
	    sender.Send(System.Text.Encoding.ASCII.GetBytes(broadcastMsg), broadcastMsg.Length);
	    Quaternion test = Quaternion.identity;

	}


	public string GetLocalIP()
	{
	    IPHostEntry host;
	    string localIP = "";
	    host = Dns.GetHostEntry(Dns.GetHostName());
	    foreach (IPAddress ip in host.AddressList)
	    {
	        if (ip.AddressFamily == AddressFamily.InterNetwork)
	        {
	            localIP = ip.ToString();
	            break;
	        }
	    }
	    Debug.Log("setting IP: " + localIP);
	    return localIP;
	}


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
