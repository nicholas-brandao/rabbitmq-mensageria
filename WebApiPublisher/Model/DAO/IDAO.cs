using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPublisher.Model.DAO
{
    public interface IDAO<T>
    {

        void Salvar(T entity);

    }
}
