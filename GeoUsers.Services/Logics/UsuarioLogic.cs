using AutoMapper;
using GeoUsers.Services.Model.Entities;
using GeoUsers.Services.Utils;
using NHibernate;
using NHibernate.Criterion;
using System;

namespace GeoUsers.Services.Logics
{
    public class UsuarioLogic : BaseLogic
    {
        public UsuarioLogic(IMapper autoMapper,
                            ISessionFactory sessionFactory) : base(autoMapper, sessionFactory)
        { }

        public bool LogIn(string loginId, string password)
        {
            var encryptedPassword = DataEncriptionUtils.Encrypt(password);

            var user = Session.QueryOver<Usuario>()
                              .WhereRestrictionOn(x => x.LoginId)
                              .IsInsensitiveLike(loginId, MatchMode.Exact)
                              .And(x => x.Password == encryptedPassword)
                              .RowCount();

            if (user == 0) throw new Exception("El nombre de usuario o la contraseña son inválidos");

            return true;
        }
    }
}
