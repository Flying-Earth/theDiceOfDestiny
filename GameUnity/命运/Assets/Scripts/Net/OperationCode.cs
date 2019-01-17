using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Net
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
