using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Collections.Generic;

using Com.BaseLibrary.Service;
using Com.BaseLibrary.Data;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.DataAccess;

namespace Jufine.Backend.Security.DataAccess
{
    public static class GeneralQueryDA
    {
        public static DataTable ExecuteGeneralQuery(string tableName, string selectedField, string orderField, string whereField, int pageSize, int pageIndex,out int totalCount)
        {

            using (SecurityEntities entities = new SecurityEntities())
            {
                entities.ExecuteFunction("UP_GeneralPagedQuery",
                 BuildParameter("ID", tableName),
                 BuildParameter("ToStatus", selectedField),
                 BuildParameter("EditUser", orderField),
                 BuildParameter("ID", whereField),
                 BuildParameter("ToStatus", pageSize),
                 BuildParameter("EditUser", pageIndex),
                 BuildParameter("EditUser", pageIndex),
                 BuildParameter("EditUser", pageIndex)
                );


            }
        }

        private static  ObjectParameter BuildParameter(string parameterName, object value)
        {
            return new ObjectParameter(parameterName, value ?? DBNull.Value);
        }
    }
}
