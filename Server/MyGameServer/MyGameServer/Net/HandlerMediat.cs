using System.Collections.Generic;
using System;
using Photon.SocketServer;
using MyGameServer.Net;
using MyGameServer;

public class HandlerMediat
{

    public delegate void Act(MyClient peer, OperationRequest operationRequest, SendParameters sendParameters);

    static Dictionary<OperationCode, Delegate> messageTable = new Dictionary<OperationCode, Delegate>();

    public static void AddListener(OperationCode type, Act act)
    {
        if (!messageTable.ContainsKey(type))
        {
            messageTable.Add(type, null);
        }

        Delegate d = messageTable[type];
        if (d != null && d.GetType() != act.GetType())
        {
            MyServer.log.Info(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", type, d.GetType().Name, act.GetType().Name));
        }
        else
        {
            messageTable[type] = (Act)messageTable[type] + act;
        }
    }

    public static void RemoveListener(OperationCode type, Act act)
    {
        if (messageTable.ContainsKey(type))
        {
            Delegate d = messageTable[type];

            if (d == null)
            {
                MyServer.log.Info(string.Format("Attempting to remove listener with for event type \"{0}\" but current listener is null.", type));
            }
            else if (d.GetType() != act.GetType())
            {
                MyServer.log.Info(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", type, d.GetType().Name, act.GetType().Name));
            }
            else
            {
                messageTable[type] = (Act)messageTable[type] - act;
                if (d == null)
                {
                    messageTable.Remove(type);
                }
            }
        }
        else
        {
            MyServer.log.Info(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", type));
        }
    }

    public static void Dispatch(OperationCode type, MyClient peer, OperationRequest operationRequest, SendParameters sendParameters)
    {
        Delegate d;
        if (messageTable.TryGetValue(type, out d))
        {
            Act callback = d as Act;

            if (callback != null)
            {
                callback(peer, operationRequest, sendParameters);
            }
            else
            {
                MyServer.log.Info(string.Format("no such event type {0}", type));
            }
        }
    }

    public static void RemoveAllListener(OperationCode type)
    {
        if (messageTable.ContainsKey(type))
        {
            messageTable[type] = null;
            messageTable.Remove(type);
        }
        else
        {
            MyServer.log.Info(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", type));
        }
    }
}