using MySql.Data.MySqlClient;
using NHibernate.Exceptions;
using System;

namespace GeoUsers.Services.SQLExceptions
{
    public class SqlExceptionConverter : ISQLExceptionConverter
    {
        public Exception Convert(AdoExceptionContextInfo adoExceptionContextInfo)
        {
            var sqlException = adoExceptionContextInfo.SqlException as MySqlException;

            if (sqlException != null)
            {
                if (sqlException.Number == 1062)
                {
                    return new UniqueConstraintViolationException(sqlException.Message, sqlException);
                }
            }

            return adoExceptionContextInfo.SqlException;
        }
    }
}
