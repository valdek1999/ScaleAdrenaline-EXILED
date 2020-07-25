using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleAdrenaline
{
    class UsedItem
    {
        public UsedMedicalItemEventArgs item;
        public bool status;

        public UsedItem(UsedMedicalItemEventArgs item)
        {
            this.item = item;
            this.status = true;
        }



        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            UsedItem temp = (UsedItem)obj;

            if (item.Player.Nickname == temp.item.Player.Nickname && status == temp.status)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
