using System.Data;
using System.Data.SqlClient;

namespace SqlMaster
{
    public static class Parameters
    {
        public static class Client
        {
            public static SqlParameter ClientIdParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@ClientId", SqlDbType.VarChar, 10, value);
            }
        }


        public static class CabinetFiles
        {
            public static SqlParameter FileIdParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@FileId", SqlDbType.Int, value);
            }

            public static SqlParameter ClientIdParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@ClientId", SqlDbType.VarChar, 10, value);
            }

            public static SqlParameter QuickPickupIdParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@QuickPickupId", SqlDbType.VarChar, 20, value);
            }
        }

        public static class IboArchive
        {
            public static SqlParameter ClientIdParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@ClientID", SqlDbType.VarChar, 50, value);
            }
        }

        public static class IboHeader
        {
            public static SqlParameter BranchCodeParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@BranchCode", SqlDbType.VarChar, 50, value);
            }

            public static SqlParameter ClientIdParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@ClientID", SqlDbType.VarChar, 10, value);
            }

            public static SqlParameter CreatedByParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@CreatedBy", SqlDbType.VarChar, 255, value);
            }

            public static SqlParameter IboGuidParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@IboGuid", SqlDbType.VarChar, 33, value);
            }

            public static SqlParameter ImportFileIdParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@ImportFileId", SqlDbType.VarChar, 255, value);
            }
        }

        public static class Screen
        {
            public static SqlParameter FieldNameParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@FieldName", SqlDbType.VarChar, 50, value);
            }
        }

        public static class Session
        {
            public static SqlParameter UIdParamater(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@UId", SqlDbType.VarChar, 255, value);
            }

            public static SqlParameter UserNameParamater(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@UserName", SqlDbType.VarChar, 255, value);
            }
        }

        public static class Users
        {
            public static SqlParameter UsernameParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@UserName", SqlDbType.VarChar, 75, value);
            }
        }

        public static class WizGlobal
        {
            public static SqlParameter ClientOnlyParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@ClientOnly", SqlDbType.VarChar, 500, value);
            }

            public static SqlParameter InvCodeParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@InvCode", SqlDbType.VarChar, 10, value);
            }

            public static SqlParameter LoanProgramParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@LoanProg", SqlDbType.VarChar, 255, value);
            }

            public static SqlParameter SearchCriteriaParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@SearchCriteria", SqlDbType.VarChar, 255, value);
            }

            public static SqlParameter UserIdParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@UserId", SqlDbType.Int, value);
            }

            public static SqlParameter WizTypeParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@WizType", SqlDbType.VarChar, 1, value);
            }
        }

        public static class WizLocal
        {
            public static SqlParameter AliasParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@Alias", SqlDbType.VarChar, 255, value);
            }

            public static SqlParameter ClientOnlyParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@ClientOnly", SqlDbType.VarChar, 500, value);
            }

            public static SqlParameter InvCodeParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@InvCode", SqlDbType.VarChar, 50, value);
            }

            public static SqlParameter SearchCriteriaParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@SearchCriteria", SqlDbType.VarChar, 255, value);
            }

            public static SqlParameter WizTypeParameter(string value)
            {
                return SqlMaster.Helper.CreateSqlParameter("@WizType", SqlDbType.VarChar, 1, value);
            }
        }
    }
}
