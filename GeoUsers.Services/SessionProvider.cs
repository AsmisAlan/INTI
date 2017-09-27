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

            public bool ExceptionThrown { get; set; }

            public ContextSessionBlock(ISession session)
                : base()
            {
                Session = session;
            }

            public void SetAction(Action action)
            {
                Action = action;
            }
        }

        readonly ISessionFactory sessionFactory;

        public SessionProvider(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public ContextSessionBlock GetSessionContextBlock()
        {
            var session = sessionFactory.OpenSession();

            session.BeginTransaction();
            ThreadStaticSessionContext.Bind(session);

            var sessionBlock = new ContextSessionBlock(session);

            sessionBlock.SetAction(() => Unbind(sessionBlock.ExceptionThrown));

            return sessionBlock;
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
