using NHibernate;
using NHibernate.Context;
using System;

namespace GeoUsers.Services
{
    public class SessionProvider
    {
        public class ContextSessionBlock : OnDisposeAction
        {
            public ISession Session { get; protected set; }

            public ContextSessionBlock(Action action, ISession session)
                : base(action)
            {
                //session.ThrowIfNull(nameof(session));

                Session = session;
            }
        }

        readonly ISessionFactory sessionFactory;

        public SessionProvider(ISessionFactory sessionFactory)
        {
            //sessionFactory.ThrowIfNull(nameof(sessionFactory));

            this.sessionFactory = sessionFactory;
        }

        public ContextSessionBlock GetSessionContextBlock()
        {
            var session = sessionFactory.OpenSession();

            session.BeginTransaction();
            ThreadStaticSessionContext.Bind(session);

            return new ContextSessionBlock(() => Unbind(), session);
        }

        public void Unbind(bool exception = false)
        {
            //Unbind session from current context
            var session = ThreadStaticSessionContext.Unbind(sessionFactory);
            if (session == null) return;

            try
            {
                var transaction = session.Transaction;

                if (TransactionWasManipulated(transaction)) return;

                if (!exception)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }

                transaction.Dispose();
            }
            finally
            {
                session.Close();
            }
        }

        bool TransactionWasManipulated(ITransaction transaction)
        {
            bool wasManipulated = transaction == null ||
                                  !transaction.IsActive ||
                                  transaction.WasRolledBack;

            if (wasManipulated) return true;

            return false;
        }
    }
}
