using NHibernate;
using NHibernate.Cfg;
using System;
using System.Reflection;

namespace PlayerInfo
{
    public sealed class SessionFactory
    {
        private static volatile ISessionFactory iSessionFactory;
        private static object syncRoot = new Object();
        public static ISession OpenSession
        {
            get
            {
                if (iSessionFactory == null)
                {
                    lock (syncRoot)
                    {
                        if (iSessionFactory == null)
                        {
                            var configuration = new Configuration();
                            configuration.Configure();
                            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
                            return sessionFactory.OpenSession();
                        }
                    }
                }
                return iSessionFactory.OpenSession();
            }
        }
    }
}
