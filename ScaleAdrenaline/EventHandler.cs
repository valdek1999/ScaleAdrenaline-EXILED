using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MEC;
using Mirror;
using Exiled.Events.EventArgs;
using Exiled.API.Features;
using Object = UnityEngine.Object;

namespace ScaleAdrenaline
{
    public class EventHandler
    {
        System.Random random = new System.Random();
        readonly public Plugin plugin;
        List<UsedItem> usedItems = new List<UsedItem>();
        List<Pickup> items = new List<Pickup>();
        public EventHandler(Plugin plugin)
        {
            this.plugin = plugin;            
        }

        public List<CoroutineHandle> coroutines = new List<CoroutineHandle>();

        public EventHandler()
        { 
       
        }

        internal void RoundStart()
        {
            
            foreach (Pickup item in Object.FindObjectsOfType<Pickup>())
            {
                if (item.ItemId.ToString() == "Adrenaline")
                {
                    items.Add(item);
                }
            }
            for (int i = items.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                // обменять значения data[j] и data[i]
                var temp = items[j];
                items[j] = items[i];
                items[i] = temp;
            }
            for(int i = plugin.Config.MaxCountAdrenaline;i<items.Count;i++)
            {
                items[i].Delete();
            }

            foreach (CoroutineHandle handle in coroutines)
            {
                Timing.KillCoroutines(handle);
            }
        }

        //internal void CountAdrenaline(UsedMedicalItemEventArgs ev)
        //{
        //    foreach (Pickup item in Object.FindObjectsOfType<Pickup>())
        //    {
        //        if (item.ItemId.ToString() == "Adrenaline")
        //        {
        //            Log.Info("id: " + item.ItemId + "\nname: " + item.name);
        //        }
                
        //    }
        //}

        internal void UsedAdrenaline(UsedMedicalItemEventArgs item)
        {
            Log.Info(item.Item.ToString());
            if (item.Item.ToString()== "Adrenaline")
            {
                UsedItem usedItem = new UsedItem(item);
                if (!usedFind(item))
                {
                    usedItems.Add(usedItem);
                    coroutines.Add(Timing.RunCoroutine(SetPlayerScale(item), "ScaleThread"));
                }
            }

        }

        private bool usedFind(UsedMedicalItemEventArgs item)
        {
            var name = item.Player.Nickname;
            foreach (var t in usedItems)
            {
                if (name == t.item.Player.Nickname)
                    return true;
            }
            return false;
        }

        public IEnumerator<float> SetPlayerScale(UsedMedicalItemEventArgs item)
        {

            foreach(var t in plugin.Config.Step1)
            {
                item.Player.Scale = Vector3.one * t;
                yield return Timing.WaitForSeconds(plugin.Config.TimeAllSteps);
            }
            yield return Timing.WaitForSeconds(plugin.Config.Duration);
            foreach (var t in plugin.Config.Step2)
            {
                item.Player.Scale = Vector3.one * t;
                yield return Timing.WaitForSeconds(plugin.Config.TimeAllSteps);
            }
            yield return Timing.WaitForSeconds(plugin.Config.Duration);
            foreach (var t in plugin.Config.Step3)
            {
                item.Player.Scale = Vector3.one * t;
                yield return Timing.WaitForSeconds(plugin.Config.TimeAllSteps);
            }
            yield return Timing.WaitForSeconds(plugin.Config.Duration);
            usedItems.RemoveAt(0);
            if(usedItems.Count == 0)
                foreach (CoroutineHandle handle in coroutines)
                {
                    Timing.KillCoroutines(handle);
                }
        }
        internal void WaitingForPlayers()
        {
            foreach (CoroutineHandle handle in coroutines)
            {
                Timing.KillCoroutines(handle);
            }
        }
    }

    
}
