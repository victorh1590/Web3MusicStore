namespace Web3MusicStore.API.Data.UnitOfWork;

public interface IUnitOfWork
{
    void Commit();
    void Rollback();
}