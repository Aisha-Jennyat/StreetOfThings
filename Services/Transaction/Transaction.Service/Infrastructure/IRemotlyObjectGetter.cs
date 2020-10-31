﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Models;

namespace Transaction.Service.Infrastructure
{
    public interface IRemotlyObjectGetter 
    {
        Task<OfferedObject?> GetObject(int objectId);
    }
}
