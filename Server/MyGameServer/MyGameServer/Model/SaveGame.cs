using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameServer.Model
{
    class SaveGame
    {
        public virtual int Id { get; set; }
        public virtual string SaveGameName { get; set; }
        public virtual string UserName { get; set; }
        public virtual int Health { get; set; }
        public virtual int Attack { get; set; }
        public virtual int Armor { get; set; }
        public virtual int Charge { get; set; }
        public virtual int MaxHealth { get; set; }
        public virtual int FullCharge { get; set; }
        public virtual int CardNum { get; set; }
        public virtual bool TransitionA { get; set; }
        public virtual bool TransitionB { get; set; }
    }
}
