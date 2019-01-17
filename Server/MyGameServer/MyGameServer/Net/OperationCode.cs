using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameServer.Net
{
    public enum OperationCode : byte
    {
        Login,
        Register,
        SaveGame,
        LoadGame,
        SaveCardSequence,
        LoadCardSequence,
        Default
    }
}
