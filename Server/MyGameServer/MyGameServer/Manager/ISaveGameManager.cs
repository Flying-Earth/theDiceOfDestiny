using MyGameServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameServer.Manager
{
    interface ISaveGameManager
    {
        void Add(SaveGame saveGame);
        void Update(SaveGame saveGame);//更新数据
        void Remove(SaveGame saveGame); //删除数据
        SaveGame GetById(int id); //根据ID获取数据
        SaveGame GetBySaveGameName(string saveGameName); //根据save game name获取数据
        ICollection<SaveGame> GetAllSaveGames();  //获取所有数据
        bool VerifySaveGame(string saveGameName, string userName);
    }
}
