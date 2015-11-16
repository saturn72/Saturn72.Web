﻿namespace Saturn72.Web.Framework.KendoUi
{
    public class DataSourceRequest
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public DataSourceRequest()
        {
            Page = 1;
            PageSize = 10;
        }
    }
}
