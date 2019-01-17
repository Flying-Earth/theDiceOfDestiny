using MyGameServer.Manager;
using MyGameServer.Model;
using MyGameServer.Net;
using MyGameServer.Tools;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameServer.Handler
{
    class SaveGameHandler : IHandlerBase
    {
        // 添加监听
        public void AddListener()
        {
            HandlerMediat.AddListener(OperationCode.SaveGame, OnSaveGameReceived);
            HandlerMediat.AddListener(OperationCode.LoadGame, OnLoadGameReceived);
            HandlerMediat.AddListener(OperationCode.SaveCardSequence, OnSaveCardSequenceReceived);
            HandlerMediat.AddListener(OperationCode.LoadCardSequence, OnLoadCardSequenceReceived);
        }

        // 移除监听
        public void RemoveListener()
        {
            HandlerMediat.RemoveListener(OperationCode.SaveGame, OnSaveGameReceived);
            HandlerMediat.RemoveListener(OperationCode.LoadGame, OnLoadGameReceived);
            HandlerMediat.RemoveAllListener(OperationCode.SaveCardSequence);
            HandlerMediat.RemoveAllListener(OperationCode.LoadCardSequence);
        }

        private void OnLoadGameReceived(MyClient peer, OperationRequest operationRequest, SendParameters sendParameters)
        {
            string saveGameName = DictTool.GetValue<byte, object>(operationRequest.Parameters, 1) as string;
            string userName = DictTool.GetValue<byte, object>(operationRequest.Parameters, 2) as string;
            //连接数据库进行校验
            SaveGameManager manager = new SaveGameManager();
            bool isExist = manager.VerifySaveGame(saveGameName, userName);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            //如果验证成功，把成功的结果利用response.ReturnCode返回成功给客户端
            if (isExist)
            {
                //从数据库读取存储信息
                SaveGame saveGame = manager.GetBySaveGameName(saveGameName);
                Dictionary<byte, object> data = new Dictionary<byte, object>();
                data.Add(1, saveGame.SaveGameName);
                data.Add(2, saveGame.UserName);
                data.Add(3, saveGame.Health);
                data.Add(4, saveGame.Attack);
                data.Add(5, saveGame.Armor);
                data.Add(6, saveGame.Charge);
                data.Add(7, saveGame.MaxHealth);
                data.Add(8, saveGame.FullCharge);
                data.Add(9, saveGame.CardNum);
                data.Add(10, saveGame.TransitionA);
                data.Add(11, saveGame.TransitionB);

                response.ReturnCode = (short)ReturnCode.Success;
                response.SetParameters(data);
            }
            else//读取失败
            {
                response.ReturnCode = (short)ReturnCode.Failed;
            }
            //把上面的回应给客户端
            peer.SendOperationResponse(response, sendParameters);
        }

        private void OnSaveGameReceived(MyClient peer, OperationRequest operationRequest, SendParameters sendParameters)
        {
            //根据发送过来的请求获得存储数据
            string saveGameName = DictTool.GetValue<byte, object>(operationRequest.Parameters, 1) as string;
            string userName = DictTool.GetValue<byte, object>(operationRequest.Parameters, 2) as string;
            int health = (int)DictTool.GetValue<byte, object>(operationRequest.Parameters, 3);
            int attack = (int)DictTool.GetValue<byte, object>(operationRequest.Parameters, 4);
            int armor = (int)DictTool.GetValue<byte, object>(operationRequest.Parameters, 5);
            int charge = (int)DictTool.GetValue<byte, object>(operationRequest.Parameters, 6);
            int maxHealth = (int)DictTool.GetValue<byte, object>(operationRequest.Parameters, 7);
            int fullCharge = (int)DictTool.GetValue<byte, object>(operationRequest.Parameters, 8);
            int cardNum = (int)DictTool.GetValue<byte, object>(operationRequest.Parameters, 9);
            bool transitionA = (bool)DictTool.GetValue<byte, object>(operationRequest.Parameters, 10);
            bool transitionB = (bool)DictTool.GetValue<byte, object>(operationRequest.Parameters, 11);
            //连接数据库进行校验
            SaveGameManager manager = new SaveGameManager();
            bool isExist = manager.VerifySaveGame(saveGameName, userName);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            //如果验证成功，把成功的结果利用response.ReturnCode返回成功给客户端
            if (!isExist)
            {
                //添加存储信息进数据库
                SaveGame saveGame = new SaveGame()
                {
                    SaveGameName = saveGameName,
                    UserName = userName,
                    Health = health,
                    Attack = attack,
                    Armor = armor,
                    Charge = charge,
                    MaxHealth = maxHealth,
                    FullCharge = fullCharge,
                    CardNum = cardNum,
                    TransitionA = transitionA,
                    TransitionB = transitionB
                };
                manager.Add(saveGame);
                response.ReturnCode = (short)ReturnCode.Success;
            }
            else//更新存储信息
            {
                SaveGame saveGame = new SaveGame()
                {
                    Id = manager.GetBySaveGameName(saveGameName).Id,
                    SaveGameName = saveGameName,
                    UserName = userName,
                    Health = health,
                    Attack = attack,
                    Armor = armor,
                    Charge = charge,
                    MaxHealth = maxHealth,
                    FullCharge = fullCharge,
                    CardNum = cardNum,
                    TransitionA = transitionA,
                    TransitionB = transitionB
                };
                manager.Update(saveGame);
                response.ReturnCode = (short)ReturnCode.Failed;
            }
            //把上面的回应给客户端
            peer.SendOperationResponse(response, sendParameters);
        }

        private void OnLoadCardSequenceReceived(MyClient peer, OperationRequest operationRequest, SendParameters sendParameters)
        {
            string saveGameName = DictTool.GetValue<byte, object>(operationRequest.Parameters, 1) as string;
            string userName = DictTool.GetValue<byte, object>(operationRequest.Parameters, 2) as string;
            //连接数据库进行校验
            SaveGameManager manager = new SaveGameManager();
            bool isExist = manager.VerifySaveGame(saveGameName, userName);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            //如果验证成功，把成功的结果利用response.ReturnCode返回成功给客户端
            if (isExist)
            {
                //从数据库读取存储信息
                int idSaveGame = manager.GetBySaveGameName(saveGameName).Id;
                Dictionary<byte, object> card = new Dictionary<byte, object>();
                SaveCardSequenceManager cardSequenceManager = new SaveCardSequenceManager();
                IList<SaveCardSequence> saveCardSequences = cardSequenceManager.GetAllIdSaveGame(idSaveGame);
                for(int i = 1; i <= saveCardSequences.Count; i++)
                {
                    card.Add((byte)i, saveCardSequences[i - 1].CardSequence);
                }
                response.ReturnCode = (short)ReturnCode.Success;
                response.SetParameters(card);
            }
            else//读取失败
            {
                response.ReturnCode = (short)ReturnCode.Failed;
            }
            //把上面的回应给客户端
            peer.SendOperationResponse(response, sendParameters);
        }

        private void OnSaveCardSequenceReceived(MyClient peer, OperationRequest operationRequest, SendParameters sendParameters)
        {
            //根据发送过来的请求获得存储数据
            string saveGame = DictTool.GetValue<byte, object>(operationRequest.Parameters, 1) as string;
            //连接数据库进行校验
            SaveCardSequenceManager manager = new SaveCardSequenceManager();
            SaveGameManager saveGameManager = new SaveGameManager();
            int idSaveGame = 0;
            bool isExist = false;
            idSaveGame = saveGameManager.GetBySaveGameName(saveGame).Id;
            isExist = manager.VerifySaveGame(idSaveGame);
            SaveCardSequence saveCard = new SaveCardSequence();
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            //如果验证成功，把成功的结果利用response.ReturnCode返回成功给客户端
            if (!isExist)
            {
                saveCard.IdSaveGame = idSaveGame;
                //添加存储信息进数据库
                for(int i = 2; i <= operationRequest.Parameters.Count; i++)
                {
                    saveCard.CardSequence = (int)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)i);
                    manager.Add(saveCard);
                }                            
                response.ReturnCode = (short)ReturnCode.Success;
            }
            else//更新存储信息
            {
                IList<SaveCardSequence> saveCardSequences = manager.GetAllIdSaveGame(idSaveGame);
                saveCard.IdSaveGame = saveCardSequences.First().IdSaveGame;
                saveCard.Id = saveCardSequences.First().Id;
                for (int i = 2; i <= operationRequest.Parameters.Count; i++)
                {   
                    saveCard.CardSequence = (int)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)i);
                    manager.Update(saveCard);
                    saveCard.Id ++;
                }
                response.ReturnCode = (short)ReturnCode.Failed;
            }
            //把上面的回应给客户端
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
