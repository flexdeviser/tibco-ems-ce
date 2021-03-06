/* 
 * Copyright (c) 2001-2016 TIBCO Software Inc. 
 * All Rights Reserved. Confidential & Proprietary.
 * For more information, please contact:
 * TIBCO Software Inc., Palo Alto, California, USA
 * 
 * $Id: tibemsSSL.cs 90180 2016-12-13 23:00:37Z $
 * 
 */

/*
 * This is a simple sample of SSL connectivity for
 * EMS .NET clients.  It demonstrates ssl-based client/server communications.
 *
 * This sample establishes an SSL connection, publishes and
 * receives back three messages.
 *
 *      -server     Server URL passed to the ConnectionFactory
 *                  constructor.
 *                  If not specified this sample assumes a
 *                  serverUrl of "ssl://localhost:7243"
 *
 *
 * Example usage:
 *
 *  tibemsSSL -server ssl://localhost:7243
 *
 * This sample can also demonstrate full use of SSL with the mutual
 * authentication. The following command line will cause the client
 * to verify the server certificate and also establishes the client
 * identity which will be verified by the server. In the sample
 * command line below the client-side SSL trace is being enabled,
 * if you also running the server with -ssl_debug_trace parameter
 * you will see the entire authentication trace:
 *
 * tibemsSSL -ssl_trace
 *                -ssl_trusted ../certs/server_root.cert.pem
 *                -ssl_target_hostname server
 *                -ssl_identity ../certs/client_identity.p12
 *                -ssl_password password
 */
 
using System;
using System.Collections;
using TIBCO.EMS;

public class tibemsSSL
{
    String      serverUrl       = "ssl://localhost:7243";
    String      userName        = null;
    String      password        = null;
    String      topicName       = "topic.sample";

    // SSL options
    bool        ssl_trace                = false;
    ArrayList   ssl_trusted              = new ArrayList();
    String      ssl_target_hostname      = null;
    String      ssl_identity             = null;
    String      ssl_password             = null;


    public tibemsSSL(String[] args) {

        parseArgs(args);

        if (topicName == null) {
            System.Console.WriteLine("Error: must specify topic name");
            usage();
        }

        System.Console.WriteLine("SSL sample.");

        // initialize SSL environment
        Hashtable environment = new Hashtable();
        try {
            EMSSSLFileStoreInfo storeInfo = new EMSSSLFileStoreInfo();

            // set trace for client-side operations, loading of certificates
            // and other
            if (ssl_trace) {
                environment.Add(EMSSSL.TRACE, true);
            }

            // set trusted certificates if specified
            int s = ssl_trusted.Count;
            for(int i=0;i<s;i++){
                String cert = (String)ssl_trusted[i];
                storeInfo.SetSSLTrustedCertificate(cert);
            }

            // set expected host name in the certificate if specified
            if (ssl_target_hostname != null) {
                environment.Add(EMSSSL.TARGET_HOST_NAME, ssl_target_hostname);
            }

            // only pkcs12 or pfx files are supported.
            if (ssl_identity != null) {
                if (ssl_password == null) {
                    System.Console.WriteLine("Error: must specify -ssl_password if identity is set");
                    System.Environment.Exit(-1);
                }
                storeInfo.SetSSLClientIdentity(ssl_identity);
                storeInfo.SetSSLPassword(ssl_password.ToCharArray());

            }
            environment.Add(EMSSSL.STORE_INFO, storeInfo);
            environment.Add(EMSSSL.STORE_TYPE, EMSSSLStoreType.EMSSSL_STORE_TYPE_FILE);
        }
        catch(Exception e) 
        {
            System.Console.WriteLine("ERROR MSG: " + e.Message);
            System.Console.WriteLine(e.StackTrace);

            if (e is EMSException) 
            {
                EMSException je = (EMSException)e;
                if (je.LinkedException != null) 
                {
                    System.Console.WriteLine("Linked Exception Error Msg: " + e.Message);
                    System.Console.WriteLine("##### Linked Exception:");
                    System.Console.WriteLine(je.LinkedException.StackTrace);
                }
            }
            System.Environment.Exit(-1);
        }

        try
        {

            ConnectionFactory factory = new ConnectionFactory(serverUrl,null,environment);

            Connection connection = factory.CreateConnection(userName,password);

            Session session = connection.CreateSession(false,TIBCO.EMS.SessionMode.AutoAcknowledge);

            Topic topic = session.CreateTopic(topicName);

            MessageProducer publisher  = session.CreateProducer(topic);
            MessageConsumer subscriber = session.CreateConsumer(topic);

            connection.Start();

            MapMessage message = session.CreateMapMessage();
            message.SetStringProperty("field","SSL message");

            for (int i=0; i<3; i++) {
                publisher.Send(message);
                System.Console.WriteLine("\nPublished message: "+message);

                /* read same message back */

                message = (MapMessage)subscriber.Receive();
                if (message == null)
                    System.Console.WriteLine("\nCould not receive message");
                else
                    System.Console.WriteLine("\nReceived message: "+message);

                try { 
                    System.Threading.Thread.Sleep(1000); 
                }
                catch(Exception){}
            }

            connection.Close();
        }
        catch(EMSException e)
        {
            System.Console.WriteLine("ERROR MSG: " + e.Message);
            System.Console.WriteLine(e.StackTrace);

            if (e.LinkedException != null) 
            {
                System.Console.WriteLine("Linked Exception Error Msg: " + e.Message);
                System.Console.WriteLine("##### Linked Exception:");
                System.Console.WriteLine(e.LinkedException.StackTrace);
            }
            System.Environment.Exit(-1);
        }
    }

    public static void Main (string []args)
    {
        tibemsSSL t = new tibemsSSL(args);
    }

    void usage()
    {
        System.Console.WriteLine("\nUsage: tibemsSSL [options]");
        System.Console.WriteLine("");
        System.Console.WriteLine("   where options are:");
        System.Console.WriteLine("");
        System.Console.WriteLine(" -server        <server URL>   - EMS server URL, default is ssl://localhost:7243");
        System.Console.WriteLine(" -user          <user name>    - user name, default is null");
        System.Console.WriteLine(" -password      <password>     - password, default is null");
        System.Console.WriteLine(" -topic         <topic-name>   - topic name, default is \"topic.sample\"");
        System.Console.WriteLine("");
        System.Console.WriteLine("  SSL options:");
        System.Console.WriteLine("");
        System.Console.WriteLine("                                     default is 'j2se'");
        System.Console.WriteLine(" -ssl_trace                        - trace SSL initialization");
        System.Console.WriteLine(" -ssl_trusted      <file-name>     - file with trusted certificate(s),");
        System.Console.WriteLine("                                     this parameter may repeat if more");
        System.Console.WriteLine("                                     than one file required");
        System.Console.WriteLine(" -ssl_target_hostname     <host-name>     - name expected in the server certificate");
        System.Console.WriteLine(" -ssl_identity     <file-name>     - client identity file");
        System.Console.WriteLine(" -ssl_password     <string>        - password to decrypt client identity");
        System.Console.WriteLine("                                     or key file");
        System.Environment.Exit(-1);
    }

    void parseArgs(String[] args)
    {
        int i=0;

        while(i < args.Length)
        {
            if (args[i].CompareTo("-server")==0)
            {
                if ((i+1) >= args.Length) usage();
                serverUrl = args[i+1];
                i += 2;
            }
            else
            if (args[i].CompareTo("-topic")==0)
            {
                if ((i+1) >= args.Length) usage();
                topicName = args[i+1];
                i += 2;
            }
            else
            if (args[i].CompareTo("-user")==0)
            {
                if ((i+1) >= args.Length) usage();
                userName = args[i+1];
                i += 2;
            }
            else
            if (args[i].CompareTo("-password")==0)
            {
                if ((i+1) >= args.Length) usage();
                password = args[i+1];
                i += 2;
            }
            else
            if (args[i].CompareTo("-ssl_trace")==0)
            {
                ssl_trace = true;
                i += 1;
            }
            else
            if (args[i].CompareTo("-ssl_trusted")==0)
            {
                if ((i+1) >= args.Length) usage();
                ssl_trusted.Add(args[i+1]);
                i += 2;
            }
            else
            if (args[i].CompareTo("-ssl_target_hostname")==0)
            {
                if ((i+1) >= args.Length) usage();
                ssl_target_hostname = args[i+1];
                i += 2;
            }
            else
            if (args[i].CompareTo("-ssl_identity")==0)
            {
                if ((i+1) >= args.Length) usage();
                ssl_identity = args[i+1];
                i += 2;
            }
            else
            if (args[i].CompareTo("-ssl_password")==0)
            {
                if ((i+1) >= args.Length) usage();
                ssl_password = args[i+1];
                i += 2;
            }
            else
            if (args[i].CompareTo("-help")==0)
            {
                usage();
            }
            else
            {
                System.Console.WriteLine("Unrecognized parameter: "+args[i]);
                usage();
            }
        }
    }

}


