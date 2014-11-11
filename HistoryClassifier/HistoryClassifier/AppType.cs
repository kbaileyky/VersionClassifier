using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryClassifier
{
    interface AppType
    {


        string Get_String();
        int Get_Int();

    }

    class Desktop : AppType
    {
        public string str_type;
        public appType enum_type;
        public int int_type;

        public Desktop()
        {
            str_type = "Desktop";
            enum_type = appType.Desktop;
            int_type = 0;
        }

        public string Get_String()
        {
            return str_type;
        }

        public int Get_Int()
        {
            return int_type;
        }

    }

    class Mobile : AppType
    {
        public string str_type;
        public appType enum_type;
        public int int_type;

        public Mobile()
        {
            str_type = "Mobile";
            enum_type = appType.Mobile;
            int_type = 1;
        }

        public string Get_String()
        {
            return str_type;
        }

        public int Get_Int()
        {
            return int_type;
        }

    }

    class Sibling : AppType
    {
        public string str_type;
        public appType enum_type;
        public int int_type;

        public Sibling()
        {
            str_type = "Sibling";
            enum_type = appType.Sibling;
            int_type = 2;
        }

        public string Get_String()
        {
            return str_type;
        }

        public int Get_Int()
        {
            return int_type;
        }

    }

    class AppNotClassified : AppType
    {
        public string str_type;
        public appType enum_type;
        public int int_type;

        public AppNotClassified()
        {
            str_type = "Not Classified";
            enum_type = appType.NotClassified;
            int_type = 3;
        }

        public string Get_String()
        {
            return str_type;
        }

        public int Get_Int()
        {
            return int_type;
        }

    }


}
