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
    class SaveCardSequenceManager : ISaveCardSequenceManager
    {
        public void Add(SaveCardSequence saveCardSequence)
        {
            //也可使用成一个事务
            using (ISession session = NhibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())//事务的开始
                {
                    //进行操作
                    session.Save(saveCardSequence);
                    transaction.Commit();//事物的提交
                }
            }
        }

        public ICollection<SaveCardSequence> GetAllSaveCardSequence()
        {
            using (ISession session = NhibernateHelper.OpenSession())
            {
                //Restrictions.Eq()表示添加查询条件
                // criteria.UniqueResult<User>();得到唯一的结果，返回的是User对象
                IList<SaveCardSequence> saveCardSequences = session.CreateCriteria(typeof(SaveCardSequence)).List<SaveCardSequence>();
                return saveCardSequences;
            }
        }

        public SaveCardSequence GetById(int id)
        {
            using (ISession session = NhibernateHelper.OpenSession())
            {
                //进行操作
                SaveCardSequence saveCardSequence = session.Get<SaveCardSequence>(id);//删除数据
                return saveCardSequence;
            }
        }

        public SaveCardSequence GetByCardNum(int cardNum)
        {
            using (ISession session = NhibernateHelper.OpenSession())
            {
                //Restrictions.Eq()表示添加查询条件
                // criteria.UniqueResult<User>();得到唯一的结果，返回的是User对象
                SaveCardSequence saveCardSequence = session.CreateCriteria(typeof(SaveCardSequence)).Add(Restrictions.Eq("CardSequence", cardNum)).UniqueResult<SaveCardSequence>();//创建一个配置文件
                return saveCardSequence;
            }
        }

        public void Remove(SaveCardSequence saveGame)
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

        public void Update(SaveCardSequence saveGame)
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

        public bool VerifySaveGame(int idSaveGame)
        {
            using (ISession session = NhibernateHelper.OpenSession())
            {
                IList<SaveCardSequence> saveCardSequences = session.CreateCriteria(typeof(SaveCardSequence))
                          .Add(Restrictions.Eq("IdSaveGame", idSaveGame)).List<SaveCardSequence>();
                if (saveCardSequences.Count == 0) return false;
                return true;
            }
        }

        public IList<SaveCardSequence> GetAllIdSaveGame(int idSaveGame)
        {
            using (ISession session = NhibernateHelper.OpenSession())
            {
                IList<SaveCardSequence> saveCardSequences = session.CreateCriteria(typeof(SaveCardSequence))
                          .Add(Restrictions.Eq("IdSaveGame", idSaveGame)).List<SaveCardSequence>();
                return saveCardSequences;
            }
        }
    }
}
