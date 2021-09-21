using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
       
        ICuentaAhorroRepository CreditoRepository { get; }
      
        int Commit();
    }
}
