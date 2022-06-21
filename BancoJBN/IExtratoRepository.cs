using System;
using System.Collections.Generic;

namespace BancoJBN
{
    public interface IExtratoRepository
    {
        IList<Extrato> GetByPeriodo(int agenciaId, int contaId, DateTime dataInicio, DateTime dataFim);
        void Save(Extrato extrato);
    }
}