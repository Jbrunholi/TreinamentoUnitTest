using System;
using System.Collections.Generic;

namespace BancoJBN
{
    public class ContaCorrente : IContaCorrente
    {
        public ContaCorrente(IAgenciaRepository agenciaRepository, IcontaRepository contaRepository, IExtratoRepository extratoRepository)
        {
            AgenciaRepository = agenciaRepository;
            ContaRepository = contaRepository;
            ExtratoRepository = extratoRepository;
        }

        public IAgenciaRepository AgenciaRepository { get; set; }

        public IcontaRepository ContaRepository { get; set; }

        public IExtratoRepository ExtratoRepository { get; set; }

        public bool Deposito(int agencia, int conta, decimal valor, out string mensagemErro)
        {
            throw new NotImplementedException();
        }

        public IList<Extrato> Extrato(int agencia, int conta, DateTime dataInicio, DateTime dataFim, out string mensagemErro)
        {
            throw new NotImplementedException();
        }

        public decimal Saldo(int agencia, int conta, out string mensagemErro)
        {
            throw new NotImplementedException();
        }

        public bool Saque(int agencia, int conta, decimal valor, out string mensagemErro)
        {
            throw new NotImplementedException();
        }

        public bool Transferencia(int agenciaOrigem, int contaOrigem, decimal valor, int agenciaPara, int contaPara, out string mensagemErro)
        {
            throw new NotImplementedException();
        }
    }
}
