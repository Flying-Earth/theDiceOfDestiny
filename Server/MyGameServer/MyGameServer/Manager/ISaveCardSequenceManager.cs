using MyGameServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameServer.Manager
{
    interface ISaveCardSequenceManager
    {
        void Add(SaveCardSequence saveGame);
        void Update(SaveCardSequence saveGame);//更新数据
        void Remove(SaveCardSequence saveGame); //删除数据
        SaveCardSequence GetById(int id); //根据ID获取数据
        SaveCardSequence GetByCardNum(int cardNum);
        ICollection<SaveCardSequence> GetAllSaveCardSequence();  //获取所有数据
        IList<SaveCardSequence> GetAllIdSaveGame(int idSaveGame);  //获取所有数据
        bool VerifySaveGame(int idSaveGame);
    }
}
