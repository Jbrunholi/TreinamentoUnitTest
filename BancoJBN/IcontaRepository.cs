namespace BancoJBN
{
    public interface IcontaRepository
    {
        Conta GetById(int agenciaId, int contaId);

        void Save(Conta conta);
    }
}