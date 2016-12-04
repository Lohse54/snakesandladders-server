﻿using System;

namespace PSCC_Server
{


    //Class newClient
    public class newClient
    {
        bool active, active1, yes = true;
        List<TcpClient> listOfClients = new List<TcpClient>();
        string[] players = { "Player1", "Player2", "Player3" };
        int playerSelect = -1, playerNum = 0;
        int port = 1615;
        TcpListener listener;


        //Listening to new client
        public newClient()
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            addClients();
        }
        //Accepts a new client and starts it on a thread
        public void addClients()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                listOfClients.Add(client);
                Thread newThread = new Thread(new ParameterizedThreadStart(communication));
                newThread.Start(client);
            }
        }

        public void communication(object obj)
        {
            // Writer and Reader assigned
            TcpClient client1 = (TcpClient)obj;
            StreamWriter writer = new StreamWriter(client1.GetStream(), Encoding.ASCII) { AutoFlush = true };
            StreamReader reader = new StreamReader(client1.GetStream(), Encoding.ASCII);


            while (true)
            {
                //Assigns name to the client that has been accepted
                string inputLine = "";
                playerSelect += 1;
                if (playerSelect > 2)
                {
                    playerSelect = 0;
                }
                Console.WriteLine("Client : " + players[playerSelect] + " has been initialized");
                writer.WriteLine(players[playerSelect]);
                inputLine = reader.ReadLine();
                writer.WriteLine(players[playerSelect]);
                //Loop that wait until all clients have been accepted
                while (yes)
                {
                    inputLine = reader.ReadLine();
                    writer.WriteLine(players[playerSelect]);
                    if (playerSelect > 1)
                    {
                        yes = false;
                    }
                }
                playerNum = 0;
                active = true;
                active1 = true;


            }
        }
