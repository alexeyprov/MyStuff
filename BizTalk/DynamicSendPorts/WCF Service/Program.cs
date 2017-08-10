//----------------------------------------------------------------------
// File: Program.cs
// 
// Summary: The code for the WCF service application.
//
// Sample: Using Dynamic Send Ports with the WCF Adapters (BizTalk Server Sample) 
//
//----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server 2009 release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Xml;
using System.ServiceModel.Channels;

namespace Service
{
    [ServiceContract]
    public interface IReceiveMessage
    {
        [OperationContract(ReplyAction = "*")]
        void ReceiveMessage(Message message);
    }

    public class MessageReceiver : IReceiveMessage
    {
        #region IReceiveMessage Members

        public void ReceiveMessage(Message message)
        {
            WriteInfoToConsole(message);
        }

        #endregion

        #region util

        /// <summary>
        /// Dumps the content of a WCF message to console
        /// </summary>
        /// <param name="message"></param>
        private void WriteInfoToConsole(Message message)
        {
            lock (Console.Out) //need to synchronize Console.Out otherwise more than one thread may try to write to it.
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Message received by " + OperationContext.Current.Channel.LocalAddress);
                Console.WriteLine();
                Console.WriteLine("### MESSAGE CONTENT ###");
                XmlTextWriter writer = new XmlTextWriter(Console.Out);
                writer.Formatting = Formatting.Indented;
                message.WriteMessage(writer);
                writer.Close();
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        #endregion util
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(MessageReceiver)))
            {
                serviceHost.Open();

                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                serviceHost.Close(TimeSpan.FromSeconds(20));
            }
        }
    }
}

