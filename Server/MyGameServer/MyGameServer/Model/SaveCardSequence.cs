using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameServer.Model
{
    class SaveCardSequence
    {
        public virtual int Id { get; set; }
        public virtual int IdSaveGame { get; set; }
        public virtual int CardSequence { get; set; }
    }
}
