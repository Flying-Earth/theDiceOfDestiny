using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameServer.Model;
using NHibernate;
using NHibernate.Criterion;

namespace MyGameServer.Manager
{
    class SaveGameManager : ISaveGameManager
    {
        public void Add(SaveGame saveGame)
        {
            //也可使用成一个事务
            using (ISession session = NhibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())//事务的开始
                {
                    //进行操作
                    session.Save(saveGame);
                    transaction.Commit();//事物的提交
                }
            }
        }

        public ICollection<SaveGame> GetAllSaveGames()
        {
            using (ISession session = NhibernateHelper.OpenSession())
            {
                //Restrictions.Eq()表示添加查询条件
                // criteria.UniqueResult<User>();得到唯一的结果，返回的是User对象
                IList<SaveGame> saveGames = session.CreateCriteria(typeof(SaveGame)).List<SaveGame>();


                return saveGames;
            }
        }

        public SaveGame GetById(int id)
        {
            using (ISession session = NhibernateHelper.OpenSession())
            {
                //进行操作
                SaveGame saveGame = session.Get<SaveGame>(id);//删除数据
                return saveGame;
            }
        }

        public SaveGame GetBySaveGameName(string saveGameName)
        {
            using (ISession session = NhibernateHelper.OpenSession())
            {
                //Restrictions.Eq()表示添加查询条件
                // criteria.UniqueResult<User>();得到唯一的结果，返回的是User对象
                SaveGame saveGame = session.CreateCriteria(typeof(SaveGame)).Add(Restrictions.Eq("SaveGameName", saveGameName)).UniqueResult<SaveGame>();//创建一个配置文件
                return saveGame;
            }
        }

        public void Remove(SaveGame saveGame)
        {
            using (ISession session = NhibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())//事务的开始
                {
                    //进行操作
                    session.Delete(saveGame);//删除数据
                    transaction.Commit();//事物的提交
                }
            }
        }

        public void Update(SaveGame saveGame)
        {
            using (ISession session = NhibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())//事务的开始
                {
                    //进行操作
                    session.Update(saveGame);//更新数据
                    //session.Delete(saveGame);
                    //session.Save(saveGame);
                    transaction.Commit();//事物的提交
                }
            }
        }

        public bool VerifySaveGame(string saveGameName, string userName)
        {
            using (ISession session = NhibernateHelper.OpenSession())
            {
                IList<SaveGame> users = session.CreateCriteria(typeof(SaveGame)).List<SaveGame>();
                SaveGame user = session.CreateCriteria(typeof(SaveGame))
                          .Add(Restrictions.Eq("SaveGameName", saveGameName))
                          .Add(Restrictions.Eq("UserName", userName))
                          .UniqueResult<SaveGame>();
                if (user == null) return false;
                return true;
            }
        }
    }
}
