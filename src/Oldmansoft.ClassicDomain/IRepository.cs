﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ClassicDomain
{
    /// <summary>
    /// 全部
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TDomain, TKey> :
        IAdd<TDomain>,
        IReplace<TDomain>,
        IRemove<TDomain>,
        IGet<TDomain, TKey>,
        IRepositoryAdd<TDomain, TKey>,
        IRepositoryAddReplace<TDomain, TKey>
        where TDomain : class
    {
    }
}
