using System.Data;

namespace ClinicWeb.Core.UnitOfWork
{

    public class UnitOfWork : IUnitOfWork
    {
        private IDbTransaction _transaction;
        private IDbConnection _Connection;


        public UnitOfWork(IDbConnection connection)
        {
            _transaction = connection.BeginTransaction(IsolationLevel.Serializable);
            _Connection = connection;

        }

        public IDbTransaction Transaction => _transaction;

        public void Commit()
        {
            try
            {
                _transaction?.Commit();

            }
            catch (Exception ex)
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _Connection.Close();
                //_Connection?.Dispose();
                _transaction = null;

            }
        }

        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
                _Connection?.Close();
            }
            finally
            {
                _transaction?.Dispose();
                //_Connection?.Dispose();
                _transaction = null;
            }
        }
    }
}
