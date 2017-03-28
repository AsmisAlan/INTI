using NHibernate.Exceptions;
using System;
using System.Data.SqlClient;

namespace GeoUsers.Services.SQLExceptions
{
    public class SqlExceptionConverter : ISQLExceptionConverter
    {
        public Exception Convert(AdoExceptionContextInfo adoExceptionContextInfo)
        {
            var sqlException = adoExceptionContextInfo.SqlException as SqlException;

            if (sqlException != null)
            {
                if (sqlException.Number == 547)
                {
                    return new ConstraintViolationException(sqlException.Message, sqlException);
                }
                else if (sqlException.Number == 2601)
                {
                    return new UniqueIndexViolationException(sqlException.Message, sqlException);
                }
            }

            return adoExceptionContextInfo.SqlException;
        }
    }
}
