using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballGame.Framework
{
    public interface IMessageTarget
    {
        int OnMessage(int from, int msgType, object data1, object data2);
    }

    public class TargetContext
    {
        public IMessageTarget Target;
        public int GroupId;
    }

    public enum MessageDest
    {
        Single = 1,
        Group = 2,
        All = 3
    }

    public class Message
    {
        public MessageDest Dest;
        public int From;
        public int To;
        public int MsgType;
        public int Time;
        public object Data1;
        public object Data2;
    }

    public class MessageManager
    {
        const int MESSAGE_QUEUED = -1;

        public List<TargetContext> Targets = new List<TargetContext>();
        public int NumTargets { get { return this.Targets.Count(); } }
        public List<Message> MessageQueue = new List<Message>();

        public int RegisterTarget(IMessageTarget target, int groupId)
        {

            // Add the new target

            var context = new TargetContext();

            context.Target = target;
            context.GroupId = groupId;

            this.Targets.Add(context);

            return this.NumTargets - 1;
        }


        public void RemoveTargets()
        {
            RemoveAllQueuedMessages();
            this.Targets.Clear();
        }


        public void Update()
        {
            foreach (var msg in this.MessageQueue)
            {
                msg.Time--;
            }

            foreach (var msg in this.MessageQueue.Where(m => m.Time <= 0))
            {
                DispatchMessage(msg);
                this.MessageQueue.Remove(msg);
            }
        }

        public int SendMessage(int from, int to, int msgType, int time, object data1, object data2)
        {
            var msg = new Message();

            msg.Dest = MessageDest.Single;
            msg.From = from;
            msg.To = to;
            msg.MsgType = msgType;
            msg.Time = time;
            msg.Data1 = data1;
            msg.Data2 = data2;


            // If time is expired, dispatch now
            // Otherwise, queue it

            if (time <= 0)
                return DispatchMessage(msg);
            else
                AddQueuedMessage(msg);


            // Return -1 for queued messages

            return MESSAGE_QUEUED;

        }
        
        public int SendMessageToGroup(int from, int toGroup, int msgType, int time, object data1, object data2)
        {
            var msg = new Message();

            if (toGroup == (int)MessageDest.All)
                msg.Dest = MessageDest.All;
            else
                msg.Dest = MessageDest.Group;
       
            msg.From = from;
            msg.To = toGroup;
            msg.MsgType = msgType;
            msg.Time = time;
            msg.Data1 = data1;
            msg.Data2 = data2;


            // If time is expired, dispatch now
            // Otherwise, queue it

            if (time <= 0)
                return DispatchMessage(msg);
            else
                AddQueuedMessage(msg);


            // Return -1 for queued messages

            return MESSAGE_QUEUED;
        }
        
        public int DispatchMessage(Message msg)
        {
            switch (msg.Dest)
            {
                case MessageDest.Single:
                    return DispatchMessageToSingle(msg);

                case MessageDest.Group:
                    return DispatchMessageToGroup(msg);

                case MessageDest.All:
                    return DispatchMessageToAll(msg);

                default:
                    return -1;
            }
        }
        
        public int DispatchMessageToSingle(Message msg)
        {
            // Just return if the message is designated for an invalid target

            if (msg.To < 0 || msg.To >= NumTargets)
                return -1;

            // Dispatch the mesage to the target

            return this.Targets[msg.To].Target.OnMessage(msg.From, msg.MsgType, msg.Data1, msg.Data2);
        }

        public int DispatchMessageToGroup(Message msg)
        {

            // Dispatch the message to targets that within the group

            foreach (var target in this.Targets)
            {
                if (target.GroupId == msg.To)
                {
                    target.Target.OnMessage(msg.From, msg.MsgType, msg.Data1, msg.Data2);
                }
            }

            return -1;
        }

        public int DispatchMessageToAll(Message msg)
        {
            // Dispatch the message to all registered targets

            foreach (var target in this.Targets)
            {
                target.Target.OnMessage(msg.From, msg.MsgType, msg.Data1, msg.Data2);
            }

            return -1;
        }

        public void AddQueuedMessage(Message msg)
        {
            this.MessageQueue.Add(msg);
        }

        public void RemoveAllQueuedMessages()
        {
            this.MessageQueue.Clear();
        }

        public void ForceMessages(int id, int sentBy)
        {
            var msgs = this.MessageQueue.Where(m => m.Dest == MessageDest.Single);

            foreach (var msg in msgs)
            {
                DispatchMessage(msg);
                this.MessageQueue.Remove(msg);
            }
        }

        public void ForceGroupMessages(int id, int sentBy)
        {
            var msgs = this.MessageQueue.Where(m => m.Dest == MessageDest.Group);

            foreach (var msg in msgs)
            {
                DispatchMessage(msg);
                this.MessageQueue.Remove(msg);
            }
        }

        public void FlushMessages(int id, int sentBy)
        {
            var msgs = this.MessageQueue.Where(m => m.Dest == MessageDest.Single);

            foreach (var msg in msgs)
            {
                this.MessageQueue.Remove(msg);
            }
        }

        public void FlushGroupMessages(int id, int sentBy)
        {
            var msgs = this.MessageQueue.Where(m => m.Dest == MessageDest.Group);

            foreach (var msg in msgs)
            {
                this.MessageQueue.Remove(msg);
            }
        }
                
    }
}
