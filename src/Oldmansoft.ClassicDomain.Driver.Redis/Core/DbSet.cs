﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Oldmansoft.ClassicDomain.Driver.Redis.Core
{
    /// <summary>
    /// 实体集
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    internal abstract class DbSet<TDomain, TKey> : IDbSet<TDomain, TKey>
    {
        /// <summary>
        /// 领域名称
        /// </summary>
        private string DomainName { get; set; }

        /// <summary>
        /// 更改列表
        /// </summary>
        protected ChangeList<TDomain> List { get; set; }

        private ConcurrentQueue<Func<IDatabase, bool>> ExecuteList { get; set; }

        /// <summary>
        /// 主键表达式
        /// </summary>
        protected Func<TDomain, TKey> KeyExpression { get; private set; }

        /// <summary>
        /// 连接
        /// </summary>
        protected ConfigItem Config { get; private set; }

        /// <summary>
        /// 创建实体集
        /// </summary>
        /// <param name="config"></param>
        /// <param name="keyExpression"></param>
        public DbSet(ConfigItem config, Func<TDomain, TKey> keyExpression)
        {
            DomainName = typeof(TDomain).FullName;
            List = new ChangeList<TDomain>();
            ExecuteList = new ConcurrentQueue<Func<IDatabase, bool>>();
            Config = config;
            KeyExpression = keyExpression;
        }

        /// <summary>
        /// 拼接 Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string MergeKey(TKey key)
        {
            string id;
            if (key is Guid) id = ((Guid)Convert.ChangeType(key, typeof(Guid))).ToString("N");
            else id = key.ToString();

            return string.Format("{0}:{1}", DomainName, id);
        }

        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        protected string GetKey(TDomain domain)
        {
            return MergeKey(KeyExpression(domain));
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract TDomain Get(TKey id);

        /// <summary>
        /// 注册添加
        /// </summary>
        /// <param name="domain"></param>
        public void RegisterAdd(TDomain domain)
        {
            List.Addeds.Enqueue(domain);
        }

        /// <summary>
        /// 注册替换
        /// </summary>
        /// <param name="domain"></param>
        public void RegisterReplace(TDomain domain)
        {
            List.Updateds.Enqueue(domain);
        }

        /// <summary>
        /// 注册移除
        /// </summary>
        /// <param name="domain"></param>
        public void RegisterRemove(TDomain domain)
        {
            List.Deleteds.Enqueue(domain);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        public abstract int Commit();
    }
}